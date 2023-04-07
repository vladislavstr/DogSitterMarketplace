using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DogSitterMarketplaceDal.Models.Pets
{
    public class PetEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 2)]
        public string Characteristics { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public AnimalTypeEntity Type { get; set; }

        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public UserEntity User { get; set; }

        public int UserId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        //public virtual ICollection<PetsInOrderEntity> PetsInOrder { get; set; }

        public List<OrderEntity> Orders { get; } = new();
    }
}
