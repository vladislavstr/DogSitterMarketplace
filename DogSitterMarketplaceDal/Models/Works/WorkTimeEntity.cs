using System.ComponentModel.DataAnnotations;

namespace DogSitterMarketplaceDal.Models.Works
{
    public class WorkTimeEntity
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [DataType(DataType.Time)]
        public DateTime Stop { get; set; }
    }
}
