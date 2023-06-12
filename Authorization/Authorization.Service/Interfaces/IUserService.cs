using Authorization.Repository.Entities;
using Authorization.Repository.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(Guid Id);
        Task<User> GetUserByLogin(LoginDTO loginDTO);
        Task<Dictionary<Guid, UserDTO>> GetUsers(DefoltFilter pageFilter);
        Task<Guid> AddUser(UserDTO user);
        Task UpdateUser(Guid id, LoginDTO user);
        Task SetClientRole(Guid id);
        Task RemoveUser(Guid Id);
        Task<string> GetRole(Guid userId);
    }
}
