using DogSitterMarketplaceBll.Models.Users.Request;

namespace DogSitterMarketplaceBll.Models.Users
{
    public class UserRegister : UserRequest
    {
        public string Password { get; set; }
    }
}
