using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ITimeWorkService
    {
        public bool AddNewTimeWork(int locationWorkId, List<TimingLocationWorkRequest> timing);

        public bool AddNewTimeWork(TimingLocationWorkRequest timing);

        public bool UpdateTimeWork(TimingLocationWorkWithIdRequest timingUpdate);

        public bool DeleteTiming(int id);

        public TimingLocationWorkResponse GetTiming(int id);

        public List<TimingLocationWorkResponse> GetAllTimigsOfLocationWork(int locationId);
    }
}
