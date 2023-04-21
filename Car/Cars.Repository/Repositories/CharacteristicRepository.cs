using Cars.Repository.Entities;
using Cars.Repository.Interfaces;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Repositories
{
    public class CharacteristicRepository : Repository<Characteristic, Guid>, ICharacteristicRepository
    {
        public CharacteristicRepository(DbContext context) : base(context)
        {
        }
    }
}
