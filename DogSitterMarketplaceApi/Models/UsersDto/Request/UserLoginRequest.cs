using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceApi.Models.UsersDto.Request
{
    public class UserLoginRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
