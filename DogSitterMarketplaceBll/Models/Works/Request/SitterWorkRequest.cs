namespace DogSitterMarketplaceBll.Models.Works.Request
{
    public class SitterWorkRequest : SitterWorkBaseRequest
    {
        public int Id { get; set; }

        public List<int> LocationsWorks { get; set; }
    }
}