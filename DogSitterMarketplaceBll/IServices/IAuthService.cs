using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterUser(UserRegister userregister);

        Task<AuthResult> LoginUser(UserLogin userLogin);
    }
}
