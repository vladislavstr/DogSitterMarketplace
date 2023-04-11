using DogSitterMarketplaceApi.Models.PetsDto.Request;

namespace DogSitterMarketplaceApi.Models.UsersDto.Request
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
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
    {
    }
}
