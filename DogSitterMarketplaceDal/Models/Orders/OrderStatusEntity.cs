using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class OrderStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Comment { get; set; }
    }
}
