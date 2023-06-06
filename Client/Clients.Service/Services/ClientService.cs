using AutoMapper;
using Clients.Repository.Entities;
using Clients.Repository.Interfaces;
using Clients.Service.Interfaces;
using Infrastructure;
using Infrastructure.DTO;
using Infrastructure.DTO.ClientDTOs;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.Interfaces;
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
        private readonly IUserSessionGetter _userSessionGetter;
        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, IUserSessionGetter userSessionGetter)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
            _userSessionGetter = userSessionGetter;
        }

        public async Task<decimal?> GetBalance()
        {
            var user = await _unitOfWork.Clients.GetEntity(_userSessionGetter.UserId);
            return user.Balance;
        }
        public async Task<FIODTO> GetFIO()
        {
            var user = await _unitOfWork.Clients.GetEntity(_userSessionGetter.UserId);
            var passport = await _unitOfWork.Passports.GetEntity(user.PassportId);
            var fio = _map.Map<FIODTO>(passport);
            return fio;
        }
        public async Task<decimal?> GetBalance(Guid id)
        {
            var user = await _unitOfWork.Clients.GetEntity(id);
            return user.Balance;
        }
        public async Task<ClientContactDTO> GetClient(Guid Id)
        {
            var client = await _unitOfWork.Clients.GetEntity(Id);
            if (client == null)
            {
                throw new NotFoundException("Клиент не найден");
            }
            return _map.Map<ClientContactDTO>(client);
        }

        public async Task<Dictionary<Guid, ClientContactDTO>> GetClients(PageFilter pageFilter)
        {
            var clients = await _unitOfWork.Clients.GetAll();
            Dictionary<Guid, ClientContactDTO> result = new Dictionary<Guid, ClientContactDTO>();
            foreach (var item in clients)
            {
                result.Add(item.Id, _map.Map<ClientContactDTO>(item));
            }
            return result;
        }

        public async Task AddClient(Guid id, ClientDocumentDTO clientDTO)
        {
            var client = _map.Map<Client>(clientDTO);
            client.Id = id;
            client.DateRegistration = DateTime.Today;
            await _unitOfWork.Clients.AddEntities(client);
            await _unitOfWork.Clients.SaveChanges();
        }
        public async Task UpdateClient(Guid id, ClientDocumentDTO clientDTO)
        {
            var client = _map.Map<Client>(clientDTO);
            client.Id = id;
            _unitOfWork.Clients.UpdateEntities(client);
            await _unitOfWork.Clients.SaveChanges();
        }
        public async Task UpdateClient(ClientContactDTO clientDTO)
        {
            var client = _map.Map<Client>(clientDTO);
            client.Id = _userSessionGetter.UserId;
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

        public async Task UpdateBalance(decimal summ)
        {
            var client = await _unitOfWork.Clients.GetEntity(_userSessionGetter.UserId);
            client.Balance += summ;
            _unitOfWork.Clients.UpdateEntities(client);
            await _unitOfWork.Clients.SaveChanges();
        }
    }
}
