using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal
{
    public class OrdersAndPetsContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<AppealEntity> Appeals { get; set; }

      //  public DbSet<PetsInOrderEntity> PetsInOrders { get; set; }

        public DbSet<SitterWorkEntity> SitterWork { get; set; }

        public DbSet<LocationEntity> Location { get; set; }

        public DbSet<PetEntity> Pets { get; set; }

        public DbSet<AnimalTypeEntity> AnimalsTypes { get; set; }

        public DbSet<UserEntity> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer("sqlConnectionString");
            //builder.UseInMemoryDatabase("Db");
            builder.UseSqlServer(@"Data Source=DESKTOP-GRG9GQS;Initial Catalog=DogSitt;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProp = entityType.FindProperty("IsDeleted");
                if(isDeletedProp != null)
                {
                    isDeletedProp.SetDefaultValue(false);
                }
            }
        }
    }
}
