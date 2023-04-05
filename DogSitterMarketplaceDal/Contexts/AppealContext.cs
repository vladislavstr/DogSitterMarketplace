﻿using DogSitterMarketplaceDal.Models.Appeals;
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

        public DbSet<AppealEntity> Appeal { get; set; }
        public DbSet<AppealStatusEntity> AppealStatus { get; set; }
        public DbSet<AppealTypeEntity> AppealType { get; set; }
    }
}
