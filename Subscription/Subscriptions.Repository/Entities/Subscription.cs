using Infrastructure.HelperModels;

namespace Subscriptions.Repository.Entities
{
    public class Subscription : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityMinutsInDay { get; set; }
        public decimal Price { get; set; }
    }
}
