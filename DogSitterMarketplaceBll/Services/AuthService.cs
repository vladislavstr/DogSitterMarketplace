﻿using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Users;
using DogSitterMarketplaceDal.Configurations;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DogSitterMarketplaceBll.Services
{
    internal class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> manager;
        private readonly IAuthRepository repository;
        private readonly IJwtConfigurationSettings settings;
        private readonly IMapper mapper;

        public AuthService(
           UserManager<IdentityUser> userManager,
           IAuthRepository authRepository,
           IJwtConfigurationSettings jwtConfigurationSettings,
           IMapper autoMapper)
        {
            repository = authRepository;
            manager = userManager;
            settings = jwtConfigurationSettings;
            mapper = autoMapper;
        }

        public async Task<AuthResult> RegisterUser(UserRegister userRegister)
        {
            var existingEmail = await manager.FindByEmailAsync(userRegister.Email);
            if (existingEmail != null)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new List<string> { "Email already registred" }
                };
            }

            var user = new IdentityUser
            {
                Email = userRegister.Email,
                UserName = userRegister.Name
            };

            var isUserCreated = await manager.CreateAsync(user, userRegister.Password);

            if (!isUserCreated.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = isUserCreated.Errors.Select(error => error.Description)
                };
            }

            var roles = GetRole(userRegister.Email);
            var token = GetJwtToken(user, roles);

            var userDal = mapper.Map<UserRegister, UserEntity>(userRegister);

            var userId = repository.AddUser(userDal);

            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        public async Task<AuthResult> LoginUser(UserLogin userLogin)
        {
            var existingUser = await manager.FindByNameAsync(userLogin.UserName);

            if (existingUser == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new[] { "User not found" }
                };
            }

            var isCredentialsCorrect = await manager.CheckPasswordAsync(existingUser, userLogin.Password);
            if (!isCredentialsCorrect)
            {
                return new AuthResult
                {
                    Success = false,
                    Error = new[] { "invalid credentials" }
                };
            }

            var roles = GetRole(existingUser.Email);
            var token = GetJwtToken(existingUser, roles);

            return new AuthResult
            {
                Success = true,
                Token = token
            };
        }

        private string GetJwtToken(IdentityUser user, IEnumerable<string> userRoles)
        {
            var key = Encoding.UTF8.GetBytes(settings.Key);

            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(userRoles.Select(ur => new Claim(ur, "true")));

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(settings.TokenTimeToLiveMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var jwtTokenaHandler = new JwtSecurityTokenHandler();
            var token = jwtTokenaHandler.CreateToken(tokenDescriptor);
            return jwtTokenaHandler.WriteToken(token);
        }

        private IEnumerable<string> GetRole(string email)
        {
            return new[] { "Admin", "Sitter", "User" };
        }
    }
}
