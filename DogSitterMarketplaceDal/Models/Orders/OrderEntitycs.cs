using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Works;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1000, MinimumLength = 2)]
        public string? Comment { get; set; }

        [Required]
        public OrderStatusEntity OrderStatus { get; set; }

        [Required]
        public SitterWorkEntity SitterWork { get; set; }

        [Required]
        public int Summ { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        public LocationEntity Location { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public ICollection<CommentEntity> Comments { get; set; }

        public ICollection<AppealEntity> Appeals { get; set; }
    }
}
