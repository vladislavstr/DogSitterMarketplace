namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class TimingLocationWorkResponse
    {
        public int Id { get;set; }

        public DayOfWeekResponse DayOfWeek { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan Stop { get; set; }
    }
}