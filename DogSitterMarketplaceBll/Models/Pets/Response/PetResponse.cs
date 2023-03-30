using DogSitterMarketplaceBll.Models.Users;

namespace DogSitterMarketplaceBll.Models.Pets.Response
{
    public class PetResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public AnimalTypeResponse Type { get; set; }
        public UserResponse User { get; set; }
    }
}
