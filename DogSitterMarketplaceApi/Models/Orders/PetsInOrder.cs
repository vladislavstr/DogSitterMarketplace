using DogSitterMarketplaceApi.Models.Pets;

namespace DogSitterMarketplaceApi.Models.Orders
{
    public class PetsInOrder
    {
        public int OrderId { get; set; }
        public int PetId { get; set;}
        public Pet Pet { get; set; }
        public Order Order { get; set; }

    }
}
