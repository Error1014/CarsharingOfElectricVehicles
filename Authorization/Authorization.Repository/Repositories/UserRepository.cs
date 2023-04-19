using Authorization.Repository.Entities;
using Authorization.Repository.Interfaces;
using Infrastructure;
using Infrastructure.DTO;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Repository.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByLogin(LoginDTO loginDTO)
        {
            string HashPasword = GeneratorHash.GetHash(loginDTO.Password);
            var user = await Set.Where(x => x.Login == loginDTO.Login && HashPasword == x.Password).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Не верно введён логин или пароль");
            }

            return user;
        }
        public async Task<string> GetRole(Guid userId)
        {
            return Set.Where(x => x.Id == userId).Select(x => x.Role.Name).FirstOrDefault();
        }
    }
}
