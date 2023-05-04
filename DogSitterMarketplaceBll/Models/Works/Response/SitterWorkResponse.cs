namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class SitterWorkResponse : SitterWorkBaseResponse
    {
        public List<LocationWorkBaseResponse> LocationsWork { get; set; }
    }
}