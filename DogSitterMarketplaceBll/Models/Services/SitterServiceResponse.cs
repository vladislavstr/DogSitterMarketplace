using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Services
{
    public class SitterServiceResponse
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponse User { get; set; }

        public ServiceTypeResponse Type { get; set; }
    }
}
