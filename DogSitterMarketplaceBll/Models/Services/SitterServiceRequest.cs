using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Services
{
    public class SitterServiceRequest
    {
        public string? Comment { get; set; }

        public UserRequest User { get; set; }

        public ServiceTypeRequest Type { get; set; }
    }
}
