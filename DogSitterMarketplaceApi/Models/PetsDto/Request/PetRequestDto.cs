
namespace DogSitterMarketplaceApi.Models.PetsDto.Request
{
    public class PetRequestDto
    {
        public string Name { get; set; }

        public string Characteristics { get; set; }

        public int AnimalTypeId { get; set; }

        public int UserId { get; set; }
    }
}
