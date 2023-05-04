using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ITimeWorkRepository
    {
        public Task<List<TimingLocationWorkEntity>> AddNewTimingsLocation(List<TimingLocationWorkEntity> timings);

        public Task<TimingLocationWorkEntity> AddNewTimingLocation(TimingLocationWorkEntity timing);

        public Task<TimingLocationWorkEntity> UpdateTimingLocation(TimingLocationWorkEntity timing);

        public Task<List<DayOfWeekEntity>> GetDaysOfWeek();

        public TimingLocationWorkEntity GetTiming(int idTiming);

        public Task<List<TimingLocationWorkEntity>> GetAllTimigsOfLocationWork(int locationWorkId);

        public Task<bool> DeleteTiming(int id);

        public Task<bool> DeleteTimingByLocationWork(int locationWorkId);
    }
}
