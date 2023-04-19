using Authorization.Repository.Entities;
using Infrastructure.DTO;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<User> GetUserByLogin(LoginDTO loginDTO);
        Task<string> GetRole(Guid userId);
    }
}
