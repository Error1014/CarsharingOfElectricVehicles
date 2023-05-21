using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptions.Repository.Entities
{
    public class ClientSubscription:BaseEntity<Guid>
    {
        public Guid ClientId { get; set; }
        public Guid SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
        public DateTime DateSubscription { get; set; }
        public int QuantityMonths { get; set; }

        public ClientSubscription()
        {

        }
        public ClientSubscription(Guid clientId, Guid subscriptionId, int quantityMonths)
        {
            ClientId = clientId;
            SubscriptionId = subscriptionId;
            DateSubscription = DateTime.Today;
            QuantityMonths = quantityMonths;
        }
    }
}
