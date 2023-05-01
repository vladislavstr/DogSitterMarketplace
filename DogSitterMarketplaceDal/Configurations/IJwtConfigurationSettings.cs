namespace DogSitterMarketplaceDal.Configurations
{
    public interface IJwtConfigurationSettings
    {
        string Key { get; set; }

        int TokenTimeToLiveMinutes { get; set; }
    }
}
