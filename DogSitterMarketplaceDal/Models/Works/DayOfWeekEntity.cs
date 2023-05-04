using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Works
{
    [Index(nameof(Name), IsUnique = true)]
    public class DayOfWeekEntity : IComparable<DayOfWeekEntity>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public string Name { get; set; }

        public int CompareTo(DayOfWeekEntity day)
        {
            if (day.Id < Id)
            {
                return 1;
            }
            else if (day.Id > Id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}