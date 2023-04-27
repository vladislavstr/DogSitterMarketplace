using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IWorkAndLocationRepository
    {
        public bool AddNewLocationWork(LocationWorkEntity locationWork);

        public bool UpdateLocationWork(LocationWorkEntity updateLocationWork);

        public bool DeleteLocationWork(int locationworkId);

        public List<LocationWorkEntity> GetAllLocationWork();

        public LocationWorkEntity GetLocationWorkByid(int id);

        public SitterWorkEntity GetNotDeletedSitterWorkById(int id);

        public List<SitterWorkEntity> GetAllSitterWorksByUserId(int id);

        public LocationEntity GetLocationById(int id);

        public List<LocationWorkEntity> GetAllLocationsWorkBySitterWork(int sitterWorkId);

        public List<LocationWorkEntity> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false);

        public List<LocationWorkEntity> GetAllLocationWorkByLocation(int locationId);

        public List<LocationWorkEntity> GetAllLocationsWorkByLocationAndStatus(int locationId, bool isNotActive = false);

        public List<LocationWorkEntity> GetAllLocationWorkbyActiveStatus(bool isNotActive = false);
    }
}
