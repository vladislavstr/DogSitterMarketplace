using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Models.Contexts
{
    public class WorkContext : DbContext
    {
        public DbSet<DayOfWeekEntity> DaysOfWeek { get; set; }

        public DbSet<LocationEntity> Locations { get; set; }

        public DbSet<LocationWorkEntity> LocationWorks { get; set; }

        public DbSet<SitterWorkEntity> SitterWorks { get; set; }

        public DbSet<TimingLocationWorkEntity> TimingLocationWorks { get; set; }

        public DbSet<WorkTypeEntity> WorkTypes { get; set; }

        //public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSitterSqlConnect"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(m => m.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }

            modelBuilder.Entity<LocationEntity>().Property(l => l.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<LocationWorkEntity>().Property(lw => lw.IsNotActive).HasDefaultValue(false);
            modelBuilder.Entity<SitterWorkEntity>().Property(sw => sw.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<WorkTypeEntity>().Property(wt => wt.IsDeleted).HasDefaultValue(false);
        }
    }
}