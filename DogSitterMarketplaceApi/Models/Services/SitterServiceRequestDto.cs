using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Services
{
    public class SitterServiceRequestDto
    {
        public string? Comment { get; set; }

        public UserRequestDto User { get; set; }

        public ServiceTypeRequestDto Type { get; set; }
    }
}
