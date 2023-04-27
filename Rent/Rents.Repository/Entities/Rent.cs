using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Entities
{
    public class Rent:BaseEntity<Guid>
    {
        public Guid ClientId { get; set; }
        public Guid CarId { get; set; }
        public DateTime DateTmeBeginRent { get; set; }
        public Guid TariffId { get; set; }
        public virtual Tariff? Tariff { get; set; }
    }
}
