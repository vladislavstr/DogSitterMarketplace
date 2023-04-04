using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogSitterMarketplaceDal.Models.Pets;

[Index(nameof(Name), IsUnique = true)]
public class AnimalTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
   // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Name { get; set; }

    [Required]
    public string Parameters { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
