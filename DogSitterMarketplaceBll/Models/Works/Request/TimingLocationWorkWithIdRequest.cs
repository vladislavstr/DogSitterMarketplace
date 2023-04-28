namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class TimingLocationWorkWithIdRequest
    {
        public int Id { get; set; }

        public int? DayOfWeekId { get; set; }

        public int LocationWorkId { get; set; }

        public TimeSpan? Start { get; set; }

        public TimeSpan? Stop { get; set; }
    }
}
