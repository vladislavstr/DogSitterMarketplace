using DogSitterMarketplaceDal.Models.Contexts;
using DogSitterMarketplaceDal.Models.Users;
using Microsoft.EntityFrameworkCore;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }

        public DbSet<UserRoleEntity> UsersRoles { get; set; }

        public DbSet<UserStatusEntity> UsersStatuses { get; set; }

        // Локейшн и Ситтер НЕ УДАЛЯТЬ!
        public DbSet<LocationWorkEntity> LocationWorks { get; set; }

        public DbSet<SitterWorkEntity> SitterWorks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
        }
    }
}
