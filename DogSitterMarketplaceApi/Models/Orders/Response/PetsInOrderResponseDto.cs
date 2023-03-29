using DogSitterMarketplaceApi.Models.Pets;

namespace DogSitterMarketplaceApi.Models.Orders.Response
{
    public class PetsInOrderResponseDto
    {
        public OrderResponseDto Order { get; set; }
        public PetResponseDto Pet { get; set; }
    }
}
