namespace DogSitterMarketplaceApi.Models.Services
{
    public class WorkTimeResponseDto
    {
        public int Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}
