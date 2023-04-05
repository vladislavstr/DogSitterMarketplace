using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class OrderStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(300, MinimumLength = 2)]
        public string? Comment { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
