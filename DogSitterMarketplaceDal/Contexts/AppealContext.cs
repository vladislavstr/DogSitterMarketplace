using DogSitterMarketplaceDal.Models.Appeals;
using Microsoft.EntityFrameworkCore;

namespace DogSitterMarketplaceDal.Contexts
{
    internal class AppealContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("");
            optionsBuilder.UseInMemoryDatabase("AppealDb");
        }

        public DbSet<AppealEntity> Appeals { get; set; }
        public DbSet<AppealStatusEntity> AppealsStatuses { get; set; }
        public DbSet<AppealTypeEntity> AppealsTypes { get; set; }
    }
}

