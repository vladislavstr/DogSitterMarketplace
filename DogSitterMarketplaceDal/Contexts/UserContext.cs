using Microsoft.EntityFrameworkCore;

using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }

        public DbSet<UserRoleEntity> UsersRoles { get; set; }

        public DbSet<UserStatusEntity> UsersStatuses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
        }
    }
}
