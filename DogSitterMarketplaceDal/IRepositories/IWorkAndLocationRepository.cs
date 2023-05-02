using DogSitterMarketplaceDal.Models.Works;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IWorkAndLocationRepository
    {
        public LocationWorkEntity GetLocationWorkByid(int id);

        public Task<SitterWorkEntity> GetNotDeletedSitterWorkById(int id);

        public Task<List<SitterWorkEntity>> GetAllSitterWorksByUserId(int id);

        public Task<LocationEntity> GetLocationById(int id);

        public Task<List<SitterWorkEntity>> GetSittersWorksByThemId(List<int> sitersWorksId);
    }
}
