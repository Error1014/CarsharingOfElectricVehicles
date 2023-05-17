using Infrastructure.HelperModels;

namespace Subscriptions.Repository.Entities
{
    public class Subscription : BaseEntity<Guid>
    {
        public int QuantityMinutsInDay { get; set; }
        public decimal Price { get; set; }
    }
}
