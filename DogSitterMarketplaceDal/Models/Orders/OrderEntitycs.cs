using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Works;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1000, MinimumLength = 2)]
        public string? Comment { get; set; }

        [Required]
        [ForeignKey(nameof(OrderStatusId))]
        public OrderStatusEntity OrderStatus { get; set; }

        public int OrderStatusId { get; set; }

        [Required]
        [ForeignKey(nameof(SitterWorkId))]
        public SitterWorkEntity SitterWork { get; set; }

        public int SitterWorkId { get; set; }

        [Required]
        public int Summ { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateEnd { get; set; }

        [Required]
        [ForeignKey(nameof(LocationId))]
        public LocationEntity Location { get; set; }

        public int LocationId { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public List<CommentEntity> Comments { get; set; } = new();

        public List<AppealEntity> Appeals { get; set; } = new();

        //  public virtual ICollection<PetsInOrderEntity> PetsInOrder { get; set; }

        public List<PetEntity> Pets { get; } = new();
    }
}
