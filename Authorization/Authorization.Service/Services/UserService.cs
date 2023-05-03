using Authorization.Repository.Entities;
using Authorization.Repository.Interfaces;
using Authorization.Service.Interfaces;
using AutoMapper;
using Infrastructure;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _map = mapper;
        }
        public async Task AddUser(UserDTO userDTO)
        {
            var user = await _unitOfWork.Users.Find(u => u.Login == userDTO.Login);
            if (user !=null)
            {
                throw new DublicateException("Пользователь с таким логином уже существует");
            }
            if (userDTO.RoleId==3)
            {
                //создать соответствующую запись в Client
            }
            user = _map.Map<User>(userDTO);
            user.Password = GeneratorHash.GetHash(userDTO.Password);
            await _unitOfWork.Users.AddEntities(user);
            await _unitOfWork.Users.SaveChanges();

        }

        public async Task<IEnumerable<UserDTO>> GetUsers(PageFilter pageFilter)
        {
            var list = await _unitOfWork.Users.GetPage(pageFilter);
            var result = _map.Map<IEnumerable<UserDTO>>(list);
            return result;
        }

        public async Task<UserDTO> GetUser(Guid Id)
        {
            var user = await _unitOfWork.Users.GetEntity(Id);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            var userDTO = _map.Map<UserDTO>(user);
            return userDTO;
        }
        public async Task<User> GetUserByLogin(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.GetUserByLogin(loginDTO);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            return user;
        }

        public async Task RemoveUser(Guid Id)
        {
            var user = await _unitOfWork.Users.GetEntity(Id);
            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }
            _unitOfWork.Users.RemoveEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }

        public async Task UpdateUser(Guid id,UserDTO userDTO)
        {
            var user = _map.Map<User>(userDTO);
            user.Id = id;
            user.Password = GeneratorHash.GetHash(userDTO.Password);
            _unitOfWork.Users.UpdateEntities(user);
            await _unitOfWork.Users.SaveChanges();
        }
        public async Task<string> GetRole(Guid userId)
        {
            return await _unitOfWork.Users.GetRole(userId);
        }
    }
}
