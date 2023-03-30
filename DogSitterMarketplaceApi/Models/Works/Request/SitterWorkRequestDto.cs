namespace DogSitterMarketplaceApi.Models.Works.Request
{
    public class SitterWorkRequestDto
    {
        public string? Comment { get; set; }

        public int UserId { get; set; }

        public int WorkTypeId { get; set; }

        public List<int> LocationsWorks { get; set; }
    }
}