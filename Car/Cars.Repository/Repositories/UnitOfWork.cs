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
            CarCharacteristics = new CarCharacteristicRepository(context);
            Cars = new CarRepository(context);
            CarTags = new CarTagRepository(context);
            Characteristics = new CharacteristicRepository(context);
            Tags = new TagRepository(context);
        }

        public IBrandModelRepository BrandModels { get; private set; }
        public ICarCharacteristicRepository CarCharacteristics{ get; private set; }
        public ICarRepository Cars { get; private set; }
        public ICarTagRepository CarTags { get; private set; }
        public ICharacteristicRepository Characteristics { get; private set; }
        public ITagRepository Tags { get; private set; }

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
