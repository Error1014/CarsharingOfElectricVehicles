using AutoMapper;
using Configuration.Repository.Entities;
using Configuration.Repository.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            list = list.OrderBy(x => x.Key).ToList();
            Dictionary<Guid, ConfigurationItemDTO> result = new Dictionary<Guid, ConfigurationItemDTO>();
            foreach (var item in list)
            {
                result.Add(item.Id, _map.Map<ConfigurationItemDTO>(item));
            }
            return result;

        }

        public async Task<Guid> AddConfiguration(ConfigurationItemDTO configurationItem)
        {
            var item = await _unitOfWork.ConfigurationItems.Find(x=>x.Key==configurationItem.key);
            if (item != null)
            {
                throw new BadRequestException("Запись с таким ключём уже сужествует!!!");
            }
            var result = _map.Map<ConfigurationItem>(configurationItem);
            await _unitOfWork.ConfigurationItems.AddEntities(result);
            await _unitOfWork.ConfigurationItems.SaveChanges();
            return result.Id;
        }

        public async Task UpdateConfiguration(Guid id, ConfigurationItemDTO configurationItem)
        {
            var item = await _unitOfWork.ConfigurationItems.GetEntity(id);
            if (item == null)
            {
                throw new NotFoundException("Запись не найдена");
            }
            var result = _map.Map<ConfigurationItem>(configurationItem);
            result.Id=id;
            _unitOfWork.ConfigurationItems.UpdateEntities(result);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }
        public async Task UpdateConfiguration(string key, ConfigurationItemDTO configurationItem)
        {
            var item = await _unitOfWork.ConfigurationItems.Find(x=>x.Key==key);
            if (item == null) 
            {
                throw new NotFoundException("Запись не найдена");
            }
            var result = _map.Map<ConfigurationItem>(configurationItem);
            result.Id = item.Id;
            _unitOfWork.ConfigurationItems.UpdateEntities(result);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }

        public async Task RemoveConfiguration(Guid id)
        {
            var item = await _unitOfWork.ConfigurationItems.GetEntity(id);
            if (item == null)
            {
                throw new NotFoundException("Запись не найдена");
            }
            _unitOfWork.ConfigurationItems.RemoveEntities(item);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }

        public async Task RemoveConfiguration(string key)
        {
            var item = await _unitOfWork.ConfigurationItems.Find(x => x.Key == key);
            if (item == null)
            {
                throw new NotFoundException("Запись не найдена");
            }
            _unitOfWork.ConfigurationItems.RemoveEntities(item);
            await _unitOfWork.ConfigurationItems.SaveChanges();
        }
    }
}
