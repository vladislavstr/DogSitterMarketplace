using DogSitterMarketplaceApp.Models.Pets;

namespace DogSitterMarketplaceApp.Models.Orders
{
    public class PetsInOrder
    {
        public int OrderId { get; set; }
        public int PetId { get; set;}
        public Pet Pet { get; set; }
        public int OrderStatus { get; set; }

    }
}
