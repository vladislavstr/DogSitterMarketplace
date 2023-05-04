using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto.Response;

namespace DogSitterMarketplaceApi.Models.WorksDto
{
    public class SitterWorkBaseResponseDto
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public UserShortResponseDto User { get; set; }

        public WorkTypeResponseDto WorkType { get; set; }

    }
}