using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class LocationWorkBaseResponse
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public LocationResponse Location { get; set; }

        public bool IsNotActive { get; set; }

        public List<TimingLocationWorkResponse> TimingLocationWorks { get; set; }
    }
}
