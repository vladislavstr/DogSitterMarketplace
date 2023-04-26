using DogSitterMarketplaceDal.Models.Contexts;
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

        public List<LocationWorkEntity> GetAllLocationWorkBySitterWork(int sitterWorkId);

        public List<LocationWorkEntity> GetAllLocationWorkByLocation(int locationId);

    }
}
