using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Orders;

namespace DogSitterMarketplaceBll.Models.Orders.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public OrderStatusResponse OrderStatus { get; set; }

        public SitterWorkResponse SitterWork { get; set; }

        public int Summ { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public LocationResponse Location { get; set; }

        public List<CommentResponse> Comments { get; set; } = new();

        public List<AppealResponse> Appeals { get; set; } = new();

        public List<PetResponse> Pets { get; set; }

        public List<string> Messages { get; set; } = new();
    }
}
