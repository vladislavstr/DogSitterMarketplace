using DogSitterMarketplaceApi.Models.Pets.Request;

namespace DogSitterMarketplaceApi.Models.Users.Request
{
    public class UserRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UserPassportDataRequestDto PassportData { get; set; }
        public UserRoleRequestDto Role { get; set; }
        public UserStatusRequestDto? Status { get; set; }
        public ICollection<PetRequestDto>? Pets { get; set; }
    }
}
