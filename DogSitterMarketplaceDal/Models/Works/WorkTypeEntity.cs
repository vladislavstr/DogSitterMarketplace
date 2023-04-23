using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    [Index(nameof(Name), IsUnique = true)]
    public class WorkTypeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}