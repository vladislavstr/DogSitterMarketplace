namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class TimingLocationWorkResponse
    {
        public DayOfWeekResponse DayOfWeek { get; set; }

        public LocationWorkResponse LocationWork { get; set; }

        public WorkTimeResponse WorkTime { get; set; }
    }
}