namespace DogSitterMarketplaceApi.Models.WorksDto.Response
{
    public class TimingLocationWorkResponseDto
    {
        public int id { get; set; }

        public DayOfWeekResponseDto DayOfWeek { get; set; }

        public TimeSpan? Start { get; set; }

        public TimeSpan? Stop { get; set; }
    }
}