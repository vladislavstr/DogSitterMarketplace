namespace DogSitterMarketplaceCore.Configurations
{
    public interface IRepositorySettings
    {
        string ConnectionString { get; }

        bool IsInMemory { get; }

        string DatabaseName { get; }
    }
}
