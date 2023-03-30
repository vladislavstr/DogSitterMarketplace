using DogSitterMarketplaceApi.Models.Work.Response;
using DogSitterMarketplaceApi.Models.Work.Response;
using DogSitterMarketplaceApi.Models.Work.Response;

namespace DogSitterMarketplaceApi.Models.Work
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