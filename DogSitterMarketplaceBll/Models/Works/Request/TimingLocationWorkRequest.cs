namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class TimingLocationWorkRequest
    {
        public int? DayOfWeekId { get; set; }

        public int LocationWorkId { get; set; }

        public TimeSpan? Start { get; set; }

        public TimeSpan? Stop { get; set; }
    }
}