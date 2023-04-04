using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Pets;
public class AnimalTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Index(IsUnique = true)]
    [Obsolete]
    public int Name { get; set; }

    [Required]
    public string Parameters { get; set; }

    [Required]
    public bool IsDeleted { get; set; }
}
