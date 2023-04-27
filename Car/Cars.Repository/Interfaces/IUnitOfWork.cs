using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandModelRepository BrandModels { get; }
        ICarCharacteristicRepository CarCharacteristics { get; }
        ICarRepository Cars { get; }
        ICarTagRepository CarTags { get; }
        ICharacteristicRepository Characteristics { get; }
        ITagRepository Tags { get; }
        int Complete();
    }
}
