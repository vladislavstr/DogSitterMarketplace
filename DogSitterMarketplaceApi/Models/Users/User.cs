using DogSitterMarketplaceApi.Models.Pets;

namespace DogSitterMarketplaceApi.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UserPassportData PassportData { get; set; }
        public UserRole Role { get; set;}
        public UserStatus Status { get; set;}
        public ICollection<Pet> Pets { get; set;}
    }
}
