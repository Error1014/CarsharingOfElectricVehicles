using Clients.Repository.Entities;
using Clients.Repository.Interfaces;
using Infrastructure.Filters;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAct;

namespace Clients.Repository.Repositories
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        public ClientRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Client>> GetClients(ClientFilter clientFilter)
        {
            var query = Set;
            if (clientFilter.MinDateRegistration.HasValue)
            {
                query = query.Where(x => x.DateRegistration >= clientFilter.MinDateRegistration);
            }
            if (clientFilter.MaxDateRegistration.HasValue)
            {
                query = query.Where(x => x.DateRegistration <= clientFilter.MaxDateRegistration);
            }
            if (clientFilter.MinDateBirthday.HasValue)
            {
                query = query.Where(x => x.Passport.Birthday >= clientFilter.MinDateBirthday);
            }
            if (clientFilter.MaxDateIssuedDrivingLicense.HasValue)
            {
                query = query.Where(x => x.Passport.Birthday <= clientFilter.MaxDateBirthday);
            }
            if (clientFilter.MinDateIssuedDrivingLicense.HasValue)
            {
                query = query.Where(x => x.DrivingLicense.DateIssued >= clientFilter.MinDateIssuedDrivingLicense);
            }
            if (clientFilter.MaxDateIssuedDrivingLicense.HasValue)
            {
                query = query.Where(x => x.DrivingLicense.DateIssued <= clientFilter.MaxDateIssuedDrivingLicense);
            }
            if (!clientFilter.SearchBar.IsNullOrEmpty())
            {
                query = query.Where(x => ((x.Passport.Surname+" "+ x.Passport.Name+" "+ x.Passport.Patronymic).ToLower()).Contains(clientFilter.SearchBar.Trim().ToLower()));
            }
            query = query
               .OrderBy(x => x.Passport.Surname)
               .Skip(clientFilter.Offset)
               .Take(clientFilter.SizePage);
            return await query.ToListAsync();
        }
    }
}
