using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.ClientDTOs
{
    public class SubscribleDTO
    {
        public Guid SubscriptionId { get; set; }
        public int QuntityMonths { get; set; }
    }
}
