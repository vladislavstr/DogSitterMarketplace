using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    //public class WorkTypeEntity
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public bool IsDeleted { get; set; }
    //}

    [Index(nameof(Name), IsUnique = true)]
    public class WorkTypeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string? Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}