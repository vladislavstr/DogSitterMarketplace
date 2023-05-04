namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class SitterWorkBaseRequest
    {
        public string? Comment { get; set; }

        public int UserId { get; set; }

        public int WorkTypeId { get; set; }
    }
}