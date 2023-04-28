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
        Task<UserDTO> GetUserByLogin(LoginDTO loginDTO);
        Task<IEnumerable<UserDTO>> GetUsers(PageFilter pageFilter);
        Task AddUser(UserDTO user);
        Task UpdateUser(Guid id,UserDTO user);
        Task RemoveUser(Guid Id);
        Task<string> GetRole(Guid userId);
    }
}
