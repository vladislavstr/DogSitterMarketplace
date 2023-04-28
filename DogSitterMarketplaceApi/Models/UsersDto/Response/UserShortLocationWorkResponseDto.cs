﻿using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceApi.Models.UsersDto.Response
{
    public class UserShortLocationWorkResponseDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public List<WorkTypePriceResponseDto> WorkTypesPrices { get; set; }
    }
}
