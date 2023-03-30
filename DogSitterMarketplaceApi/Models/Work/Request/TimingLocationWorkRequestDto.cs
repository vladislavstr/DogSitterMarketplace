namespace DogSitterMarketplaceApi.Models.Work.Request
{
    public class TimingLocationWorkRequestDto
    {
        public int DayOfWeekId { get; set; }

        public int LocationWorkId { get; set; }

        public int WorkTimeId { get; set; }
    }
}