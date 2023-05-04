using Microsoft.EntityFrameworkCore;

using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceDal.Contexts
{
    public class AppealContext : DbContext
    {
        public DbSet<AppealEntity> Appeals { get; set; }

        public DbSet<AppealStatusEntity> AppealsStatuses { get; set; }

        public DbSet<AppealTypeEntity> AppealsTypes { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSitterMarketplaceDBConnect"));
        }
    }
}

