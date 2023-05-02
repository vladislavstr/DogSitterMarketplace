using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceBll.Models.Users;
using AutoMapper;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiAuthenticationProfile : Profile
    {
        public MapperApiAuthenticationProfile()
        {
            CreateMap<UserRegisterRequest, UserRegister>();
            //CreateMap<UserRegister, UserRegisterRequest>();

            CreateMap<UserLoginRequest, UserLogin>();
            //CreateMap<UserLogin, UserLoginRequest>();

            CreateMap<AuthResult, AuthResponse>();
            //CreateMap<AuthResponse, AuthResult>();
        }
    }
}
