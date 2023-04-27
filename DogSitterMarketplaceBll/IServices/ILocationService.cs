using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.IServices
{
    public interface ILocationService
    {
        public bool AddNewLocationWork(LocationWorkRequest location);

        public bool UpdateLocationWork(UpdateLocationWorkRequest location);

        public bool DeleteLocationWork(int locationWorkId);

        public List<LocationWorkResponse> GetAllLocationWork();

        public LocationWorkResponse GetLocationWorkByid(int id);

        public List<LocationWorkResponse> GetAllLocationWorkbyActiveStatus(bool isNotActive = false);

        public List<LocationWorkResponse> GetAllLocationWorkBySitterWork(int sitterWorkId);

        public List<LocationWorkResponse> GetLocationsWorkBySitterWorkAndStatus(int sitterWorkId, bool isNotActive = false);

        public List<LocationWorkResponse> GetAllLocationWorkByLocation(int locationId);

        public List<LocationWorkResponse> GetAllLocationWorkByLocationAndStatus(int locationId, bool isNotActive = false);
    }
}
