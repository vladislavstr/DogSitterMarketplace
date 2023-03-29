namespace DogSitterMarketplaceBll.Models.Work.Response
{
    public class LocationWorkResponse
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public SitterWorkRequest SitterWork { get; set; }

        public LocationResponse Location { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkResponse> TimingLocations { get; set; }

    }
}