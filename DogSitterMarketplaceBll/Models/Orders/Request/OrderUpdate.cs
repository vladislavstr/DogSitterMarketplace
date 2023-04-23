using DogSitterMarketplaceBll.Models.Appeals.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Models.Orders.Request
{
    public class OrderUpdate
    {
        public int Id { get; set; }

        public string? Comment { get; set; }

        public int OrderStatusId { get; set; }

        public int SitterWorkId { get; set; }

        public int Summ { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public int LocationId { get; set; }

        public List<int> Pets { get; set; }
    }
}
