﻿using DogSitterMarketplaceDal.Configurations;

namespace DogSitterMarketplaceApi.Configuration
{
    public class JwtConfigurationSettings : IJwtConfigurationSettings
    {
        public string Key { get; set; } = null!;

        public int TokenTimeToLiveMinutes { get; set; }
    }
}
