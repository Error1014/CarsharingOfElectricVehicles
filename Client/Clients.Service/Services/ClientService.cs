using AutoMapper;
using Clients.Repository.Entities;
using Clients.Repository.Interfaces;
using Clients.Service.Interfaces;
using Infrastructure;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }
        public async Task<ClientDTO> GetClient(Guid Id)
        {
            var client = await _unitOfWork.Clients.GetEntity(Id);
            if (client == null)
            {
                throw new NotFoundException("Клиент не найден");
            }
            return _map.Map<ClientDTO>(client);
        }

        public async Task<IEnumerable<ClientDTO>> GetClients(PageFilter pageFilter)
        {
            var clients = await _unitOfWork.Clients.GetAll();
            return _map.Map<IEnumerable<ClientDTO>>(clients);
        }

        public async Task AddClient(ClientDTO clientDTO)
        {
            var client = _map.Map<Client>(clientDTO);
            await _unitOfWork.Clients.AddEntities(client);
            await _unitOfWork.Clients.SaveChanges();
        }

        public async Task UpdateClient(ClientDTO clientDTO)
        {
            var client = _map.Map<Client>(clientDTO);
            _unitOfWork.Clients.UpdateEntities(client);
            await _unitOfWork.Clients.SaveChanges();
        }

        public async Task RemoveClient(Guid Id)
        {
            var entityDTO = await GetClient(Id);
            var entity = _map.Map<Client>(entityDTO);
            _unitOfWork.Clients.RemoveEntities(entity);
            await _unitOfWork.Clients.SaveChanges();
        }

    }
}
