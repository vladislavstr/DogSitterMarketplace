using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Appeals
{
    public class AppealStatusEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
