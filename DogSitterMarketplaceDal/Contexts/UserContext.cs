using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("");
            optionsBuilder.UseInMemoryDatabase("UserDb");
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserPassportDataEntity> UserPassportData { get; set; }
        public DbSet<UserRoleEntity> UserRole { get; set; }
        public DbSet<UserStatusEntity> UserStatus { get; set; }
    }
}
