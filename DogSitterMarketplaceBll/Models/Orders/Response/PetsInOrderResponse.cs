using DogSitterMarketplaceBll.Models.Pets.Response;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class PetsInOrderResponse
    {
        public OrderResponse Order { get; set; }
        public PetResponse Pet { get; set; }
    }
}
