using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
{
    public class OrdersAndPetsAndCommentsContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<AppealEntity> Appeals { get; set; }

        public DbSet<SitterWorkEntity> SitterWorks { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DbSet<PetEntity> Pets { get; set; }

        public DbSet<AnimalTypeEntity> AnimalsTypes { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<WorkTypeEntity> WorkTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
        }
    }
}
