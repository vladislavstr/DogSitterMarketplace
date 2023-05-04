namespace DogSitterMarketplaceApi.Models.Works.Request
{
    public class LocationWorkRequestDto
    {
        public decimal  Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool? IsNotActive { get; set; }
    }
}