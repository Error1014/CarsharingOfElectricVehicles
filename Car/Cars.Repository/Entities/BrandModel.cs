using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Repository.Entities
{
    public class BrandModel:BaseEntity<Guid>
    {
        public string Brand { get; set; }
        public string Model { get; set; }

        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
