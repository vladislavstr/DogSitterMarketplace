using DogSitterMarketplaceBll.Models.Pets.ResponseDto;

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
        public UserRoleResponse Role { get; set; }
        public UserStatusResponse Status { get; set; }
        public ICollection<PetResponse>? Pets { get; set; }
    }
}
