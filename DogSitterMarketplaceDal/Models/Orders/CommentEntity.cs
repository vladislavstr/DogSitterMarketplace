using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Orders
{
    public class CommentEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        [ForeignKey(nameof(OrderId))]
        [StringLength(1000, MinimumLength = 2)]
        public OrderEntity Order { get; set; }

        public int OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(CommentFromUserId))]
        public UserEntity CommentFromUser { get; set; }

        public int CommentFromUserId { get; set; }

        [Required]
        [ForeignKey(nameof(CommentToUserId))]

        public UserEntity CommentToUser { get; set; }

        public int CommentToUserId { get; set; }


        [Required]
        public bool IsDeleted { get; set; }
    }
}
