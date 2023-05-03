using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceApi.Models.UsersDto.Request
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
