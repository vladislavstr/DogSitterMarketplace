using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserStatusEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)"),]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(500)"),]
        public string? Comment { get; set; }
    }
}
