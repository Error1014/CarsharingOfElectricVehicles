using Infrastructure.Filters;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Rents.Repository.Entities;
using Rents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Rents.Repository.Repositories
{
    public class RentRepository : Repository<Rent, Guid>, IRentRepository
    {
        public RentRepository(DbContext context) : base(context)
        {
        }

        public async Task<Rent> GetActualRent(Guid clientId)
        {
            var rent = await Set.Where(x => x.ClientId == clientId).OrderBy(x => x.DateTimeBeginBoocking).LastAsync();
            return rent;
        }

        public async Task<IEnumerable<Rent>> GetRentHistoryPage(HistoryRentFilter filter)
        {
            var query = Set;
            if (filter.ClientId.HasValue)
            {
                query = query.Where(x => x.ClientId == filter.ClientId);
            }
            if (filter.MinKilometersOutsideTariff.HasValue)
            {
                query = query.Where(x => x.Kilometers >= filter.MinKilometersOutsideTariff);
            }
            if (filter.MaxKilometersOutsideTariff.HasValue)
            {
                query = query.Where(x => x.Kilometers <= filter.MaxKilometersOutsideTariff);
            }
            if (filter.DateTimeBeginRent.HasValue)
            {
                query = query.Where(x => x.DateTimeBeginRent >= filter.DateTimeBeginRent);
            }
            if (filter.DateTimeEndRent.HasValue)
            {
                query = query.Where(x => x.DateTimeEndRent <= filter.DateTimeEndRent);
            }
            if (filter.MinTotalPrice.HasValue)
            {
                query = query.Where(x => x.TotalPrice >= filter.MinTotalPrice);
            }
            if (filter.MaxTotalPrice.HasValue)
            {
                query = query.Where(x => x.TotalPrice <= filter.MaxTotalPrice);
            }
            query = query
                .OrderBy(x => x.Id)
                .Skip(filter.Offset)
                .Take(filter.SizePage);
            return await query.ToListAsync();
        }
    }
}
