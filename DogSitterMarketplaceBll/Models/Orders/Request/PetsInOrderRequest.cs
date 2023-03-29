using DogSitterMarketplaceBll.Models.Pets;

namespace DogSitterMarketplaceBll.Models.Orders.Request
{
    public class PetsInOrderRequest
    {
        public OrderRequest Order { get; set; }
        public PetRequest Pet { get; set; }
    }
}
