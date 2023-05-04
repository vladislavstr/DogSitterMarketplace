using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.WorksDto
{
    public class LocationWorkBaseResponseDto
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public LocationResponseDto Location { get; set; }

        public bool IsNotActive { get; set; }

    }
}