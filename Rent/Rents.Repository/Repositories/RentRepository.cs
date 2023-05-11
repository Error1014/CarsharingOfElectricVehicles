using Infrastructure.Repository;
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
    public class RentRepository : Repository<Rent, Guid>, IRentRepository
    {
        public RentRepository(DbContext context) : base(context)
        {
        }

        public async Task<Rent> GetActualBooking(Guid clientId)
        {
            var rent = Set.Where(x => x.ClientId == clientId).OrderBy(x=>x.DateTimeBeginBoocking).FirstOrDefault();
            return rent;
        }
    }
}
