using Microsoft.EntityFrameworkCore;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal
{
    public class DogSitterContext : DbContext
    {
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<PetsInOrderEntity> PetsInOrders { get; set; }

        public DbSet<SitterWorkEntity> SitterWork { get; set; }

        public DbSet<LocationEntity> Location { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer("sqlConnectionString");
            //builder.UseInMemoryDatabase("Db");
            builder.UseSqlServer(@"Data Source=DESKTOP-GRG9GQS;Initial Catalog=DBSOS;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
        }
    }
}
