using Microsoft.EntityFrameworkCore;
using Rents.Repository.Context;
using Rents.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(RentContext context)
        {
            _dbContext = context;
            Bookings = new BookingRepository(context);
            RentCheques = new RentChequeRepository(context);
            Tariffs = new TariffRepository(context);
        }

        public IBookingRepository Bookings { get; private set; }

        public IRentChequeRepository RentCheques { get; private set; }

        public ITariffRepository Tariffs { get; private set; }

        public IRentRepository Rents { get; private set; }

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
