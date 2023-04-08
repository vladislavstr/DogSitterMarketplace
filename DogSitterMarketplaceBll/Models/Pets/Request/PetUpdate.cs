
namespace DogSitterMarketplaceBll.Models.Pets.Request
{
    public class PetUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public int AnimalTypeId { get; set; }

        public int UserId { get; set; }
    }
}
