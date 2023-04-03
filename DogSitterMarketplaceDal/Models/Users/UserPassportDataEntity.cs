using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Users
{
    public class UserPassportDataEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PassportNumber { get; set; }
    }
}
