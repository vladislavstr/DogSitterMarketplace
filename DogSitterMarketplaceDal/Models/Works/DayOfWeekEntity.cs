using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    //public class DayOfWeekEntity
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }
    //}

    [Index(nameof(Name), IsUnique = true)]
    public class DayOfWeekEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string? Name { get; set; }
    }
}
