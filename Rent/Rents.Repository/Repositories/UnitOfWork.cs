using Microsoft.EntityFrameworkCore;
using Rents.Repository.Context;
using Rents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(RentContext context)
        {
            _dbContext = context;
            Tariffs = new TariffRepository(context);
            Rents = new RentRepository(context);
        }
        

        public ITariffRepository Tariffs { get; private set; }

        public IRentRepository Rents { get; private set; }

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
