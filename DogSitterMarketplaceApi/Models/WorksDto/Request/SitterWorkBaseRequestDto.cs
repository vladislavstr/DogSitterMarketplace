namespace DogSitterMarketplaceApi.Models.WorksDto.Request
{
    public class SitterWorkBaseRequestDto
    {
        public string? Comment { get; set; }

        public int UserId { get; set; }

        public int WorkTypeId { get; set; }
    }
}