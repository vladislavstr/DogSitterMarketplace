namespace DogSitterMarketplaceApi.Models.UsersDto.Response
{
    public class UserShortResponseDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }
    }
}
