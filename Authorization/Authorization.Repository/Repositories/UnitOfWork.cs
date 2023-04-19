using Authorization.Repository.Context;
using Authorization.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(UserContext context)
        {
            _dbContext = context;
            Users = new UserRepository(context);
            Roles = new RoleRepository(context);
        }

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }


        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
