namespace DogSitterMarketplaceApi.Models.WorksDto.Request
{
    public class WorkTimeRequestDto
    {
        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}