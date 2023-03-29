using DogSitterMarketplaceApi.Models.Pets.ResponseDto;

namespace DogSitterMarketplaceApi.Models.Orders.ResponseDto
{
    public class PetsInOrderResponseDto
    {
        public OrderResponseDto Order { get; set; }
        public PetResponseDto Pet { get; set; }
    }
}
