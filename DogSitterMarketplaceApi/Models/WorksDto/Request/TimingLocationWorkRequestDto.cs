using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.WorksDto.Request
{
    public class TimingLocationWorkRequestDto
    {
        public int DayOfWeekId { get; set; }

        public int LocationWorkId { get; set; }

        public int StartHour { get; set; }

        public int StartMinut { get; set; }

        public int StopHour { get; set; }

        public int StopMinut { get; set; }

    }
}