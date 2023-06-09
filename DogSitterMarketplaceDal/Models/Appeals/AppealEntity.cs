using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DogSitterMarketplaceDal.Models.Appeals
{
    public class AppealEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1000)"),]
        public string Text { get; set; }

        [Required]
        public DateTime DateOfCreate { get; set; }


        [Column(TypeName = "nvarchar(1000)"),]
        public string? ResponseText { get; set; }

        public DateTime? DateOfResponse { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public AppealTypeEntity Type { get; set; }

        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(StatusId))]
        public AppealStatusEntity Status { get; set; }

        public int StatusId { get; set; }

        [AllowNull]
        [ForeignKey(nameof(OrderId))]
        public OrderEntity? Order { get; set; }

        [AllowNull]
        public int? OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(AppealFromUserId))]
        public UserEntity AppealFromUser { get; set; }

        public int AppealFromUserId { get; set; }

        [ForeignKey(nameof(AppealToUserId))]
        public UserEntity? AppealToUser { get; set; }

        public int? AppealToUserId { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }
    }
}
