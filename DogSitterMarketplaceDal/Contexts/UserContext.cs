using Microsoft.EntityFrameworkCore;

using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Contexts
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TO5LEQA\SQLEXPRESS;Initial Catalog = DogSitterMarketplace; TrustServerCertificate=True;Integrated Security=SSPI", builder => builder.EnableRetryOnFailure());
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-TO5LEQA\SQLEXPRESS;Initial Catalog = DogSitt; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Connect Timeout = 60; Encrypt = False; TrustServerCertificate = False");
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserPassportDataEntity> UsersPassportData { get; set; }

        public DbSet<UserRoleEntity> UsersRoles { get; set; }

        public DbSet<UserStatusEntity> UsersStatuses { get; set; }
    }
}
