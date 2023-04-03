using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    [Index(nameof(Name),IsUnique =true)]
    public class DayOfWeekEntity
    {
        [Key]
        public int Id { get; set; }


        [Required, Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }
    }
}
