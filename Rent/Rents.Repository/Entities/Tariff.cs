using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rents.Repository.Entities
{
    public class Tariff : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public TimeSpan? Duration { get; set; }
        public decimal AdditionalPrice { get; set; } //цена за километр если время вышло
    }
}
