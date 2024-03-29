﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandModelRepository BrandModels { get; }
        ICarRepository Cars { get; }
        ICharacteristicRepository Characteristics { get; }
        int Complete();
    }
}
