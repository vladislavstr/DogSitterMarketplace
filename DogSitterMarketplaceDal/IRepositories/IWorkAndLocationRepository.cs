using DogSitterMarketplaceDal.Models.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceDal.IRepositories
{
    public interface IWorkAndLocationRepository
    {
        public LocationWorkEntity GetLocationWorkByid(int id);
    }
}
