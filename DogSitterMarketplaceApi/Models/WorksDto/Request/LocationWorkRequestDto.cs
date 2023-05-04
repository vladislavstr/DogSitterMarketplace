namespace DogSitterMarketplaceApi.Models.Works.Request
{
    public class LocationWorkRequestDto
    {
        public int Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool? IsNotActive { get; set; }
    }
}