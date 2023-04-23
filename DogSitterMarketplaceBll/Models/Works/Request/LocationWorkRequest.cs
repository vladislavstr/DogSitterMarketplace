using DogSitterMarketplaceBllProfile.Models.Works.Response;

namespace DogSitterMarketplaceBllProfile.Models.Works.Request
{
    public class LocationWorkRequest
    {
        public int Price { get; set; }

        public int SitterWorkId  { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkRequest> TimingLocations { get; set; }
    }
}