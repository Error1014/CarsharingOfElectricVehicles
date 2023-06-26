using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class SubscriptionDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityMinutsInDay { get; set; }
        public decimal Price { get; set; }
    }
}
