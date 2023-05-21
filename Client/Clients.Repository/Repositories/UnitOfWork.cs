using Clients.Repository.Context;
using Clients.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(ClientContext context)
        {
            _dbContext = context;
            Clients = new ClientRepository(context);
            Passports = new PassportRepository(context);
            DrivingLicenses = new DrivingLicenseRepository(context);
        }

        public IClientRepository Clients { get; private set; }

        public IPassportRepository Passports { get; private set; }

        public IDrivingLicenseRepository DrivingLicenses { get; private set; }


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
