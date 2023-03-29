using DogSitterMarketplaceApi.Models.Pets.RequestDto;

namespace DogSitterMarketplaceApi.Models.Orders.RequestDto
{
    public class PetsInOrderRequestDto
    {
        public OrderRequestDto Order { get; set; }
        public PetRequestDto Pet { get; set; }
    }
}
