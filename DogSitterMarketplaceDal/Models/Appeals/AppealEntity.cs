using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Appeals
{
    public class AppealEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1000)"),]
        public string Text { get; set; }

        //[Required]
        [ForeignKey(nameof(TypeId))]
        public AppealTypeEntity Type { get; set; }

        public int TypeId { get; set; }

        //[Required]
        [ForeignKey(nameof(StatusId))]
        public AppealStatusEntity Status { get; set; }

        public int StatusId { get; set; }

        //[Required]
        [ForeignKey(nameof(OrderId))]
        public OrderEntity? Order { get; set; }
        public int? OrderId { get; set; }

        //[Required]
        [ForeignKey(nameof(AppealFromUserId))]
        public UserEntity AppealFromUser { get; set; }

        public int AppealFromUserId { get; set; }

        //[Required]
        [ForeignKey(nameof(AppealToUserId))]
        public UserEntity? AppealToUser { get; set; }

        public int? AppealToUserId { get; set; }

        //[Required]
        public bool IsDeleted { get; set; }
    }
}
