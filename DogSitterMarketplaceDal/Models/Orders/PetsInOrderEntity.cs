using DogSitterMarketplaceDal.Models.Pets;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class PetsInOrderEntity
    {
        public OrderEntity Order { get; set; }

        public PetEntity Pet { get; set; }
    }
}
