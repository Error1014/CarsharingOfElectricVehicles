using AutoMapper;
using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;
using Infrastructure.DTO;

namespace Configuration.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        public ConfigurationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }

        public async Task<Dictionary<Guid, ConfigurationItemDTO>> GetConfiguration()
        {
            var list = await _unitOfWork.ConfigurationItems.GetAll();
            Dictionary<Guid, ConfigurationItemDTO> result = new Dictionary<Guid, ConfigurationItemDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<ConfigurationItemDTO>(item));
            }
            return result;

        }

        public async Task AddConfiguration(ConfigurationItemDTO configurationItem)
        {
            var result = _map.Map<ConfigurationItem>(configurationItem);
            await _unitOfWork.ConfigurationItems.AddEntities(result);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }

        public async Task UpdateConfiguration(Guid id, ConfigurationItemDTO configurationItem)
        {
            var result = _map.Map<ConfigurationItem>(configurationItem);
            result.Id=id;
            _unitOfWork.ConfigurationItems.UpdateEntities(result);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }
    }
}
