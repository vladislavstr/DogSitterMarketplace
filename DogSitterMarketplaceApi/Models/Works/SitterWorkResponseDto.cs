using DogSitterMarketplaceApi.Models.Users;
using DogSitterMarketplaceBll.Models.Works;

namespace DogSitterMarketplaceApi.Models.Works
{
    public class SitterWorkResponseDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponseDto User { get; set; }

        public WorkTypeResponseDto Type { get; set; }
    }
}
