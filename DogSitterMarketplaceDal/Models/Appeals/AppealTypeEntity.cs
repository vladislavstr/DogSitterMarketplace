using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Appeals
{
    public class AppealTypeEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
