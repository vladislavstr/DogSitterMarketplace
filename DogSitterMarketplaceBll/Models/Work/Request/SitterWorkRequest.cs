namespace DogSitterMarketplaceBll.Models.Work.Request
{
    public class SitterWorkRequest
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int UserId { get; set; }

        public int WorkTypeId { get; set; }
    }
}