
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceBll.Models.Users.Response
{
    public class UserShortLocationWorkResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public List<WorkTypePriceResponse> WorkTypesPrices { get; set; }
    }
}
