using DogSitterMarketplaceBll.Models.Works.Request;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ITimeWorkService
    {
        public bool AddNewTimeWork(int locationWorkId, List<TimingLocationWorkRequest> timing);

        public bool AddNewTimeWork(TimingLocationWorkRequest timing);

        public bool UpdateTimeWork(TimingLocationWorkWithIdRequest timingUpdate);
    }
}
