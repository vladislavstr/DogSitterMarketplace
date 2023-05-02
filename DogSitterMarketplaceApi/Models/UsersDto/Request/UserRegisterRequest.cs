using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceApi.Models.UsersDto.Request
{
    public class UserRegisterRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
