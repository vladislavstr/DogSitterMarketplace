namespace DogSitterMarketplaceApi.Models.Works.Response
{
    public class WorkTimeResponseDto
    {
        public int Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}
