using DogSitterMarketplaceApp.Models.User;

namespace DogSitterMarketplaceApp.Models.Pet
{
    public class Pat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Characteristics { get; set; }
        public bool IsDeleted { get; set; }
        public AnimalType Type { get; set; }
        public Users User { get; set; }
    }
}
