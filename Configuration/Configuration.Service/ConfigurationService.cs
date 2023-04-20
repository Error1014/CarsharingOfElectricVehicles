    using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;

namespace Configuration.Service
{
    public class ConfigurationService : IConfigurationService
    {
        public readonly IUnitOfWork _unitOfWork;
        public ConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ConfigurationItem>> GetConfiguration()
        {
            return await _unitOfWork.ConfigurationItems.GetAll();

        }

        public async Task AddConfiguration(ConfigurationItem configurationItem)
        {
            await _unitOfWork.ConfigurationItems.AddEntities(configurationItem);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }

        public async Task UpdateConfiguration(ConfigurationItem configurationItem)
        {
            _unitOfWork.ConfigurationItems.UpdateEntities(configurationItem);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }
    }
}
