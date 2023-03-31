using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.Works;

namespace DogSitterMarketplaceApi.Models.WorksDto
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