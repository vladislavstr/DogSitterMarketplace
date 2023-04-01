namespace DogSitterMarketplaceDal.Models.Works
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