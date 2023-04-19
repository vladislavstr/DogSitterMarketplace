using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserPassportDataEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)"),]
        public string PassportNumber { get; set; }
    }
}
