namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class LocationWorkBaseResponse
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public LocationResponse Location { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkResponse> TimingLocationWorks { get; set; }
    }
}
