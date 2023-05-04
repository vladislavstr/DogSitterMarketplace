using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.WorksDto
{
    public class SitterWorkResponseDto:SitterWorkBaseResponseDto
    {
        public List<LocationWorkResponseDto> LocationsWork { get; set; }
    }
}