using DogSitterMarketplaceApi.Models.Work.Response;
using DogSitterMarketplaceApi.Models.Users.Response;

namespace DogSitterMarketplaceApi.Models.Work
{
    public class SitterWorkResponseDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserResponseDto User { get; set; }

        public WorkTypeResponseDto WorkType { get; set; }

        public List<LocationWorkResponseDto> LocationsWorks { get; set; }
    }
}