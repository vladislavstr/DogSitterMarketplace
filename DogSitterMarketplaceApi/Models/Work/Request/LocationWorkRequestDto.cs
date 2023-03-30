using DogSitterMarketplaceApi.Models.Work.Request;

namespace DogSitterMarketplaceApi.Models.Work.Request
{
    public class LocationWorkRequestDto
    {
        public int Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkRequestDto> TimingLocations { get; set; }
    }
}