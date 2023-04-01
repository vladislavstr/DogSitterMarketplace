using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

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
            builder.UseSqlServer(@"Data Source=DESKTOP-GRG9GQS;Initial Catalog=DogSitter;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CommentEntity>()
        //        .HasOne(c => c.CommentFromUser)
        //        .WithOne()
        //        .HasForeignKey<UserEntity>(u => u.Id)
        //        .IsRequired(false)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<CommentEntity>()
        //        .HasOne(c => c.CommentToUser)
        //        .WithOne()
        //        .HasForeignKey<UserEntity>(u => u.Id)
        //        .IsRequired(false)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<PetsInOrderEntity>()
        //        .HasOne(p => p.Pet)
        //        .WithOne()
        //        .HasForeignKey<PetEntity>(p => p.Id)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<PetsInOrderEntity>()
        //        .HasOne(p => p.Order)
        //        .WithOne()
        //        .HasForeignKey<OrderEntity>(o => o.Id)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
