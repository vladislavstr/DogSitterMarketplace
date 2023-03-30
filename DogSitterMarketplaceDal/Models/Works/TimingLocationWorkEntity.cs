namespace DogSitterMarketplaceDal.Models.Works
{
    public class TimingLocationWorkEntity
    {
        public DayOfWeekEntity DayOfWeek { get; set; }

        public LocationWorkEntity LocationWork { get; set; }

        public WorkTimeEntity WorkTime { get; set; }
    }
}