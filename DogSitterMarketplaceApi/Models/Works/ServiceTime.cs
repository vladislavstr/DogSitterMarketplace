namespace DogSitterMarketplaceApi.Models.Works
{
    public class ServiceTime
    {
        public int Id { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
