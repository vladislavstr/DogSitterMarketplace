namespace DogSitterMarketplaceBllProfile.Models.Works.Response
{
    public class LocationWorkResponse
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public SitterWorkResponse SitterWork { get; set; }

        public LocationResponse Location { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkResponse> TimingLocations { get; set; }

    }
}