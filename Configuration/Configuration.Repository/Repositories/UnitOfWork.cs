using Configuration.Repository.Context;
using Configuration.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Repositories
{
    public class UnitOfWork: IUnitOfWork
{
        private readonly DbContext _dbContext;

        public UnitOfWork(ConfigurationContext context)
        {
            _dbContext = context;
            ConfigurationItems = new ConfigurationItemRepository(context);
        }

        public IConfigurationItemRepository ConfigurationItems { get; private set; }

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
