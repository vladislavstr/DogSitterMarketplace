using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IWorkAndLocationRepository
    {
        public Task<LocationWorkEntity> AddNewLocationWork(LocationWorkEntity locationWork);

        public Task<SitterWorkEntity> GetNotDeletedSitterWorkById(int id);
        public Task<LocationWorkEntity> UpdateLocationWork(LocationWorkEntity updateLocationWork);

        public Task<List<SitterWorkEntity>> GetAllSitterWorksByUserId(int id);
        public Task<bool> DeleteLocationWork(int locationworkId);

        public Task<LocationEntity> GetLocationById(int id);

        public Task<List<SitterWorkEntity>> GetSittersWorksByThemId(List<int> sitersWorksId);
        public List<LocationWorkEntity> GetAllLocationWork(bool? isNotActive = null);

        public Task<LocationWorkEntity> GetLocationWorkByid(int id);

        public Task<List<LocationWorkEntity>> GetAllLocationsWorkBySitterWork(int sitterWorkId,bool? IsNotActive=null);

        public Task<List<LocationWorkEntity>> GetAllLocationWorkByLocation(int locationId,bool? IsNotActive);

        public Task<List<LocationEntity>> GetAllLocation(bool? IsDeleted = null);

        public Task<SitterWorkEntity> AddSitterWork(SitterWorkEntity sitterWork);

        public Task<SitterWorkEntity> UpdateSitterWork(SitterWorkEntity sitterWork);

        public Task<bool> ChangeIsDeletedSitterWork(int sitterWorkId, bool isDeleted);

        public Task<SitterWorkEntity> GetInfoSitterWork(int sitterWorkId, bool? isDeleted = null);

        public List<SitterWorkEntity> GetSitterWorksUser(int userId, bool? IsDeleted = null);

        public Task<List<SitterWorkEntity>> GetSitterWorks(bool? IsDeleted = null);

        public Task<List<WorkTypeEntity>> GetAllWorkTypes(bool? IsDeleted = null);
    }
}
