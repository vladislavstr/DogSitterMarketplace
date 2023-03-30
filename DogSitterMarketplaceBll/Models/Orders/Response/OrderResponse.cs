using DogSitterMarketplaceBll.Models.Pets;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Works;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public OrderStatusResponse OrderStatus { get; set; }
        public SitterWorkResponse SitterWork { get; set; }
        public int Summ { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public LocationResponse Location { get; set; }
        public List<PetResponse> Pets { get; set; }

    }
}
