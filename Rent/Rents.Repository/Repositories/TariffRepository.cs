﻿using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Rents.Repository.Entities;
using Rents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Repositories
{
    public class TariffRepository : Repository<Tariff, Guid>, ITariffRepository
    {
        public TariffRepository(DbContext context) : base(context)
        {
        }
    }
}
