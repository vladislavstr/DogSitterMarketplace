using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Works
{
    public class SitterWorkRequestDto
    {
        public string? Comment { get; set; }

        public UserRequestDto User { get; set; }

        public WorkTypeRequestDto Type { get; set; }
    }
}
