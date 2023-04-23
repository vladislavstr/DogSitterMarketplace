using Microsoft.EntityFrameworkCore;

using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.Contexts
{
    public class AppealContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TO5LEQA\SQLEXPRESS;Initial Catalog = DogSitt; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False");
            optionsBuilder.UseInMemoryDatabase("AppealDb");
        }

        public DbSet<AppealEntity> Appeals { get; set; }

        public DbSet<AppealStatusEntity> AppealsStatuses { get; set; }

        public DbSet<AppealTypeEntity> AppealsTypes { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<UserRoleEntity> UserRole { get; set; }

        public DbSet<UserStatusEntity> UserStatus { get; set; }
    }
}

