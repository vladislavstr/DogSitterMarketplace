using DogSitterMarketplaceApi.Models.Services;
using DogSitterMarketplaceApi.Models.Users.Response;

namespace DogSitterMarketplaceApi.Models.Work
{
    public class SitterWorkResponseDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public UserResponseDto User { get; set; }

        public WorkTypeRequestDto WorkType { get; set; }

        public List<LocationWorkRequestDto> locationWork { get; set; }
    }
}