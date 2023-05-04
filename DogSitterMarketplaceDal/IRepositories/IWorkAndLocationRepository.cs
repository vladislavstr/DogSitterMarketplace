using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IWorkAndLocationRepository
    {
        public Task<LocationWorkEntity> AddNewLocationWork(LocationWorkEntity locationWork);

        public Task<LocationWorkEntity> UpdateLocationWork(LocationWorkEntity updateLocationWork);

        public Task<bool> DeleteLocationWork(int locationworkId);

        public List<LocationWorkEntity> GetAllLocationWork(bool? isNotActive = null);

        public Task<LocationWorkEntity> GetLocationWorkByid(int id);

        public Task<List<LocationWorkEntity>> GetAllLocationsWorkBySitterWork(int sitterWorkId,bool? IsNotActive=null);

        public SitterWorkEntity GetNotDeletedSitterWorkById(int id);

        //public Task<List<LocationWorkEntity>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false);

        public Task<List<LocationWorkEntity>> GetAllLocationWorkByLocation(int locationId,bool? IsNotActive);

        public LocationEntity GetLocationById(int id);

        public Task<List<LocationEntity>> GetAllLocation(bool? IsDeleted = null);

        //public Task<List<LocationEntity>> GetAllLocationByStatus(bool isDelete = false);

        //public Task<List<LocationWorkEntity>> GetAllLocationsWorkByLocationAndStatus(int locationId, bool isNotActive = false);

        //public Task<List<LocationWorkEntity>> GetAllLocationWorkbyActiveStatus(bool isNotActive = false);

        public Task<SitterWorkEntity> AddSitterWork(SitterWorkEntity sitterWork);

        public Task<SitterWorkEntity> UpdateSitterWork(SitterWorkEntity sitterWork);

        public Task<bool> ChangeIsDeletedSitterWork(int sitterWorkId, bool isDeleted);

        public Task<SitterWorkEntity> GetInfoSitterWork(int sitterWorkId, bool? isDeleted = null);

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id);

        //public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id, bool? workIsDeleted = null);

        public List<SitterWorkEntity> GetSitterWorksUser(int userId, bool? IsDeleted = null);

        //public List<SitterWorkEntity> GetSitterWorks();

        public Task<List<SitterWorkEntity>> GetSitterWorks(bool? IsDeleted = null);

        public Task<List<WorkTypeEntity>> GetAllWorkTypes(bool? IsDeleted = null);
    }
}
