namespace DogSitterMarketplaceBll.Models.Work.Request
{
    public class WorkTimeRequest
    {
        public int Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}