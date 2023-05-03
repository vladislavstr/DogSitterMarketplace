using DogSitterMarketplaceDal.Configurations;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Models.Works;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DogSitterMarketplaceDal.Contexts
{
    public class UserContext : IdentityDbContext //DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }

        public DbSet<UserRoleEntity> UsersRoles { get; set; }

        public DbSet<UserStatusEntity> UsersStatuses { get; set; }

        // Локейшн и Ситтер НЕ УДАЛЯТЬ!
        public DbSet<LocationWorkEntity> LocationWorks { get; set; }

        public DbSet<SitterWorkEntity> SitterWorks { get; set; }

        public DbSet<WorkTypeEntity> WorkTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //builder.UseSqlServer(Environment.GetEnvironmentVariable("DogSItterMarketplaseDBConnect"));
            //builder.UseInMemoryDatabase("AppealDb");
            builder.UseSqlServer(@"Data Source=DESKTOP-TO5LEQA\SQLEXPRESS;Initial Catalog = DogSItterMarketplaseDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False");
        }
    }
}
