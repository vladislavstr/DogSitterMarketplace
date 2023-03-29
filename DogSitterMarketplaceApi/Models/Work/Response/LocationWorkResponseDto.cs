using DogSitterMarketplaceApi.Models.Work;
using DogSitterMarketplaceApi.Models.Work;
using DogSitterMarketplaceApi.Models.Services;

namespace DogSitterMarketplaceApi.Models.Services
{
    public class LocationWorkResponseDto
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public SitterWorkResponseDto SitterWork { get; set; }

        public LocationResponseDto Location { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkResponseDto> TimingLocations { get; set; }
    }
}