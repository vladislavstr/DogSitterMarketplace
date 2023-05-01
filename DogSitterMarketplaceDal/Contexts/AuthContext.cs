using DogSitterMarketplaceDal.Configurations;
using DogSitterMarketplaceDal.Models.Users; 

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
{
    public class AuthContext : IdentityDbContext
    {
        private readonly IAuthRepositorySettings settings;
        
        public DbSet<UserEntity> Users { get; private set; }

        public AuthContext(IAuthRepositorySettings repositorySettings) : base()
        {
            settings = repositorySettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (settings.IsInMemory)
            {
                builder.UseInMemoryDatabase(settings.DatabaseName);
            }
            else
            {
                builder.UseSqlServer(settings.ConnectionString);
            }
        }
    }
}