using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.WorksDto
{
    public class LocationWorkResponseDto : LocationWorkBaseResponseDto
    {

        public SitterWorkBaseResponseDto SitterWork { get; set; }

        public List<TimingLocationWorkResponseDto> TimingLocationWorks { get; set; }
    }
}