namespace DogSitterMarketplaceApi.Models.WorksDto
{
    public class SitterWorkResponseDto : SitterWorkBaseResponseDto
    {
        public List<LocationWorkResponseDto> LocationsWork { get; set; }
    }
}