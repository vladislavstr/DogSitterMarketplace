using DogSitterMarketplaceApi.Models.Pets;

namespace DogSitterMarketplaceApi.Models.Orders.Request
{
    public class PetsInOrderRequestDto
    {
        public OrderRequestDto Order { get; set; }
        public PetRequestDto Pet { get; set; }

        public int OrderId { get; set; }
        public int PetId { get; set; }
    }
}
