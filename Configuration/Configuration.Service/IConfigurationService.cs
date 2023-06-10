using Configuration.Repository.Entities;
using Infrastructure.DTO;

namespace Configuration.Service
{
    public interface IConfigurationService
    {
        Task<Dictionary<Guid, ConfigurationItemDTO>> GetConfiguration();
        Task<Guid> AddConfiguration(ConfigurationItemDTO configurationItem);
        Task UpdateConfiguration(Guid id, ConfigurationItemDTO configurationItem);
        Task RemoveConfiguration(Guid id);
        Task UpdateConfiguration(string key, ConfigurationItemDTO configurationItem);
        Task RemoveConfiguration(string key);
    }
}
