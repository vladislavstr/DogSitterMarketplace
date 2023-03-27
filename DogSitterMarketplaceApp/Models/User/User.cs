using DogSitterMarketplaceApp.Models.Pet;

namespace DogSitterMarketplaceApp.Models.User
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
        public UserRoles Role { get; set;}
        public UserStatuses Status { get; set;}
        public ICollection<Pats> Pats { get; set;}
    }
}
