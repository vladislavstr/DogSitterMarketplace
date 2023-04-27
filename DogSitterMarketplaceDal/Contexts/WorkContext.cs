using DogSitterMarketplaceDal.Models.Works;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
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
            builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
        }
    }
}