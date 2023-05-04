using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Pets;

[Index(nameof(Name), IsUnique = true)]
public class AnimalTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public string Parameters { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
