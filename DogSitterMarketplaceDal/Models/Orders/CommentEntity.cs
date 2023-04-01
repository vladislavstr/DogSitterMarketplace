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
        public OrderEntity Order { get; set; }

        public UserEntity CommentFromUser { get; set; }

        public UserEntity CommentToUser { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
