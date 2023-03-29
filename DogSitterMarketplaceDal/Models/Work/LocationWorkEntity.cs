using DogSitterMarketplaceDal.Models.Work;

namespace DogSitterMarketplaceDal.Models.Work
{
    public class LocationWorkEntity
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public SitterWorkEntity SitterWork { get; set; }

        public LocationEntity Location { get; set; }

        public bool IsNotActive { get; set; }
    }
}