namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class WorkTimeResponse
    {
        public int Id { get; set; }

        public TimeOnly Start { get; set; }

        public TimeOnly Stop { get; set; }
    }
}
