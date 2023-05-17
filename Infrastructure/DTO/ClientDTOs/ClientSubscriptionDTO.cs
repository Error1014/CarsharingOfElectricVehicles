using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.ClientDTOs
{
    public class ClientSubscriptionDTO
    {
        public Guid ClientId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime DateSubscription { get; set; }
        public int QuantityMonths { get; set; }
        public DateTime DateEndSubscription { get { return DateSubscription.AddMonths(QuantityMonths); } }
    }
}
