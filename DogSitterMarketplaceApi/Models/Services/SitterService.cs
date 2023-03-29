using DogSitterMarketplaceApi.Models.Users;

namespace DogSitterMarketplaceApi.Models.Services
{
    public class SitterService
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public User User { get; set; } 
        public TypeOfService Type { get; set; }
        public ICollection<LocationService> Location { get; set; }

        public IDictionary<DayOfWeek, ServiceTime> ScheduleOfService { get; set; }
    }
}
