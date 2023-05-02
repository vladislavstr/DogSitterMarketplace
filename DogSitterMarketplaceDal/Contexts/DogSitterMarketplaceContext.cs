using DogSitterMarketplaceDal.Configurations;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
{
    public class DogSitterMarketplaceContext : IdentityDbContext //DbContext
    {

        public DbSet<AppealEntity> Appeals { get; set; }

        public DbSet<AppealStatusEntity> AppealsStatuses { get; set; }

        public DbSet<AppealTypeEntity> AppealsTypes { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<DayOfWeekEntity> DaysOfWeek { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DbSet<LocationWorkEntity> LocationWorks { get; set; }

        public DbSet<SitterWorkEntity> SitterWorks { get; set; }

        public DbSet<TimingLocationWorkEntity> TimingLocationWorks { get; set; }

        public DbSet<WorkTypeEntity> WorkTypes { get; set; }

        public DbSet<OrderStatusEntity> OrderStatuses { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        public DbSet<PetEntity> Pets { get; set; }

        public DbSet<AnimalTypeEntity> AnimalsTypes { get; set; }

        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }

        public DbSet<UserRoleEntity> UsersRoles { get; set; }

        public DbSet<UserStatusEntity> UsersStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
            builder.UseSqlServer(@"Data Source=DESKTOP-TO5LEQA\SQLEXPRESS;Initial Catalog = DogSItterMarketplaseDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(m => m.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProp = entityType.FindProperty("IsDeleted");
                if (isDeletedProp != null)
                {
                    isDeletedProp.SetDefaultValue(false);
                }
            }

            modelBuilder.Entity<LocationWorkEntity>().Property(lw => lw.IsNotActive).HasDefaultValue(false);
        }
    }
}