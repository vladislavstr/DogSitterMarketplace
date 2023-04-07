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

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }
        public DbSet<UserRoleEntity> UsersRoles { get; set; }
        public DbSet<UserStatusEntity> UsersStatuses { get; set; }
    }
}
