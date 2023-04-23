using DogSitterMarketplaceBll.Models.Users.Response;

namespace DogSitterMarketplaceBll.Models.Pets.Response
{
    public class PetResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Characteristics { get; set; }

        public AnimalTypeResponse Type { get; set; }

        public UserShortResponse User { get; set; }
    }
}
