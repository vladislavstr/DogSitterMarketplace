using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace DogSitterMarketplaceDal.Models.Works
{
    [Index(nameof(Name), IsUnique = true)]
    public class LocationEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Required]
        public bool? IsDeleted { get; set; }
    }
}