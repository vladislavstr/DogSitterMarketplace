using DogSitterMarketplaceApi.Models.Works.Response;
using DogSitterMarketplaceApi.Models.Works.Response;
using DogSitterMarketplaceApi.Models.Works.Response;

namespace DogSitterMarketplaceApi.Models.Works
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