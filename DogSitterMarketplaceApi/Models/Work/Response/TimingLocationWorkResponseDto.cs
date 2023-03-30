namespace DogSitterMarketplaceApi.Models.Work.Response
{
    public class TimingLocationWorkResponseDto
    {
        public DayOfWeekResponseDto DayOfWeek { get; set; }

        public LocationWorkResponseDto LocationWork { get; set; }

        public WorkTimeResponseDto WorkTime { get; set; }
    }
}