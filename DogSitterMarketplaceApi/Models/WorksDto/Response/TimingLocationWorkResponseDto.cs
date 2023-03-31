namespace DogSitterMarketplaceApi.Models.WorksDto.Response
{
    public class TimingLocationWorkResponseDto
    {
        public DayOfWeekResponseDto DayOfWeek { get; set; }

        public LocationWorkResponseDto LocationWork { get; set; }

        public WorkTimeResponseDto WorkTime { get; set; }
    }
}