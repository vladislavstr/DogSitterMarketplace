using DogSitterMarketplaceApi.Models.Users;
using DogSitterMarketplaceBll.Models.Services;

namespace DogSitterMarketplaceApi.Models.Services
{
    public class SitterServiceResponseDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponseDto User { get; set; }

        public ServiceTypeResponseDto Type { get; set; }
    }
}
