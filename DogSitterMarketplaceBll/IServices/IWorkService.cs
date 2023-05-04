using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface IWorkService
    {
        public Task<SitterWorkResponse> AddSitterWork(SitterWorkBaseRequest sitterWork);

        public Task<SitterWorkResponse> UpdateSitterWork(SitterWorkRequest sitterWork);

        public Task<bool> ChangeIsDeletedSitterWork(int sitterWorkId, bool isDeleted);

        public Task<SitterWorkResponse> GetInfoSitterWork(int sitterWorkId, bool? isDeleted = null);

        public List<SitterWorkResponse> GetSitterWorksUser(int userId, bool? workIsDeleted = null);

        //public List<SitterWorkResponse> GetSitterWorksUserByStatusIsDeleted(int userId, bool isDeleted = false);

        public Task<List<SitterWorkResponse>> GetSitterWorks(bool? isDeleted = null);

        //public  Task<List<SitterWorkResponse>> GetSitterWorksByStatusIsDeleted(bool isDeleted = false);

        public Task<List<WorkTypeResponse>> GetWorkTypes(bool? isDeleted = null);
    }
}
