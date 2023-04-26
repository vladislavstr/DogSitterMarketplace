using DogSitterMarketplaceBll.Models.Works.Request;
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
    }
}
