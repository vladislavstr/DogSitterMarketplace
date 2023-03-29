using DogSitterMarketplaceApi.Models.Work;

namespace DogSitterMarketplaceApi.Models.Services
{
    public class LocationWorkRequestDto
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int SitterWorkId { get; set; }

        public int LocationId { get; set; }

        public bool IsNotActive { get; set; }

    }
}