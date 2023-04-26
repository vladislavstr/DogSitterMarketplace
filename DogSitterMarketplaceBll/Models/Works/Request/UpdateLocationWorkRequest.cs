using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class UpdateLocationWorkRequest
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkRequest> TimingLocations { get; set; }
    }
}
