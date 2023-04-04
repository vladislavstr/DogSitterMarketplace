using System.ComponentModel.DataAnnotations;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceDal.Models.Appeals
{
    public class AppealEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }

        public AppealTypeEntity Type { get; set; }

        public AppealStatusEntity Status { get; set; }

        public OrderEntity? Order { get; set; }

        public UserEntity AppealFromUser { get; set; }

        public UserEntity? AppealToUser { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
