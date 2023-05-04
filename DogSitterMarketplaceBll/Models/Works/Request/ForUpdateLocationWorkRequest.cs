namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class ForUpdateLocationWorkRequest
    {
        public decimal Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkRequest> TimingLocations { get; set; }
    }
}