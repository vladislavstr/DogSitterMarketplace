using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceCore.Exceptions;
using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ITimeWorkService
    {
        public Task<List<TimingLocationWorkResponse>> AddNewTimeWork(int locationWorkId, List<TimingLocationWorkRequest> timing);

        public Task<TimingLocationWorkResponse> AddNewTimeWork(TimingLocationWorkRequest timing);

        public Task<TimingLocationWorkResponse> UpdateTimeWork(TimingLocationWorkWithIdRequest timingUpdate);

        public Task<bool> DeleteTiming(int id);

        public TimingLocationWorkResponse GetTiming(int id);

        public Task<List<TimingLocationWorkResponse>> GetAllTimigsOfLocationWork(int locationId);

        public Task<List<DayOfWeekResponse>> GetDaysOfWeek();
    }
}
