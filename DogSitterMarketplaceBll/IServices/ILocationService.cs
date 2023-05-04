using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ILocationService
    {
        public Task<LocationWorkResponse> AddNewLocationWork(LocationWorkBaseRequest location);

        public Task<LocationWorkResponse> UpdateLocationWork(LocationWorkUpdateRequest location);

        public Task<bool> DeleteLocationWork(int locationWorkId);

        public List<LocationWorkResponse> GetAllLocationWork(bool? isNotActive = null);

        public Task<LocationWorkResponse> GetLocationWorkByid(int id);

        //public  Task<List<LocationWorkResponse>> GetAllLocationWorkbyActiveStatus(bool isNotActive = false);

        //public  Task<List<LocationWorkResponse>> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false);

        public Task<List<LocationWorkResponse>> GetAllLocationWorkBySitterWork(int sitterWorkId, bool? isNotActive = null);

        public Task<List<LocationWorkResponse>> GetAllLocationWorkByLocation(int locationId, bool? isNotActive = null);

        //public  Task<List<LocationWorkResponse>> GetAllLocationWorkByLocationAndStatus(int locationId, bool isNotActive = false);

        //public Task<List<LocationResponse>> GetAllLocation();

        public Task<List<LocationResponse>> GetAllLocation(bool? IsDeleted = null);
    }
}
