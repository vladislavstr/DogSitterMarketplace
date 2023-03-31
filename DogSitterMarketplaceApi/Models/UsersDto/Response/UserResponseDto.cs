using DogSitterMarketplaceApi.Models.PetsDto.ResponseDto;
using DogSitterMarketplaceApi.Models.Users.Response;

namespace DogSitterMarketplaceApi.Models.UsersDto.Response
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public UserPassportDataResponseDto? PassportData { get; set; }

        public UserRoleResponseDto Role { get; set; }

        public UserStatusResponseDto Status { get; set; }

        public ICollection<PetResponseDto>? Pets { get; set; }
    }
}
