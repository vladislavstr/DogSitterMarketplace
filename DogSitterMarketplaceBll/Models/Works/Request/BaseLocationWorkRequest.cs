namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class LocationWorkBaseRequest
    {
        public int Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }
    }
}