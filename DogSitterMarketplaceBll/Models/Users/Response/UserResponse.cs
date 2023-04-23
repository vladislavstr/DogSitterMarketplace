using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.Models.Users.Response
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public UserPassportDataResponse? PassportData { get; set; }

        public UserRoleResponse UserRole { get; set; }

        public UserStatusResponse UserStatus { get; set; }

        public ICollection<PetResponse>? Pets { get; set; }
    }
}
