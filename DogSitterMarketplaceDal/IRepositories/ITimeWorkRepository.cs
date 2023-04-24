using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface ITimeWorkRepository
    {
        public bool AddNewTimingsLocation(List<TimingLocationWorkEntity> timing);

        public bool AddNewTimingLocation(TimingLocationWorkEntity timing);

        public bool UpdateTimingLocation(TimingLocationWorkEntity timing);

        public List<TimingLocationWorkEntity> GetAllTimigsOfLocationWork(int locationWorkId);

        public TimingLocationWorkEntity GetTiming(int idTiming);

        public bool DeleteTiming(int id);
    }
}
