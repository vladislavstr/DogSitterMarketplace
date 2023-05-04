namespace DogSitterMarketplaceBll.Models.Works.Response
{
    public class LocationWorkResponse: LocationWorkBaseResponse
    {
        public SitterWorkResponse SitterWork  { get; set; }
        
    }
}