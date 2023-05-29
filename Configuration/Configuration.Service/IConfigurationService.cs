using Configuration.Repository.Entities;
using Infrastructure.DTO;

namespace Configuration.Service
{
    public interface IConfigurationService
    {
        Task<Dictionary<Guid, ConfigurationItemDTO>> GetConfiguration();
        Task AddConfiguration(ConfigurationItemDTO configurationItem);
        Task UpdateConfiguration(Guid id, ConfigurationItemDTO configurationItem);
        Task UpdateConfiguration(string key, ConfigurationItemDTO configurationItem);
    }
}
