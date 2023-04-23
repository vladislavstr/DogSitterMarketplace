namespace DogSitterMarketplaceBll.Models.Pets.Request
{
    public class PetRequest
    {
        public string Name { get; set; }

        public string Characteristics { get; set; }

        public int AnimalTypeId { get; set; }

        public int UserId { get; set; }
    }
}
