using DogSitterMarketplaceDal.Models.Pets;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class PetsInOrderEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public OrderEntity Order { get; set; }

        [Required]
        public PetEntity Pet { get; set; }
    }
}
