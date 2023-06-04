using Cars.Repository.Context;
using Cars.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(CarContext context)
        {
            _dbContext = context;
            BrandModels = new BrandModelRepository(context);
            Cars = new CarRepository(context);
            Characteristics = new CharacteristicRepository(context);
        }

        public IBrandModelRepository BrandModels { get; private set; }
        public ICarRepository Cars { get; private set; }
        public ICharacteristicRepository Characteristics { get; private set; }

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
