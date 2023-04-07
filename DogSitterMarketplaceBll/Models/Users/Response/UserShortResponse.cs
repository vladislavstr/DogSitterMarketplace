using DogSitterMarketplaceBll.Models.Pets.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogSitterMarketplaceBll.Models.Users.Response
{
    public class UserShortResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }
    }
}
