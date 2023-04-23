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

        public UserPassportDataResponseDto? UserPassportData { get; set; }

        public UserRoleResponseDto UserRole { get; set; }

        public UserStatusResponseDto UserStatus { get; set; }

        //public ICollection<PetResponseDto>? Pets { get; set; }
    }
}
