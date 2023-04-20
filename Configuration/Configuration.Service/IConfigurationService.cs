using Configuration.Repository.Entities;

namespace Configuration.Service
{
    public interface IConfigurationService
    {
        Task<IEnumerable<ConfigurationItem>> GetConfiguration();
        Task AddConfiguration(ConfigurationItem configurationItem);
        Task UpdateConfiguration(ConfigurationItem configurationItem);
    }
}
