using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cars.Repository.Entities
{
    public class Car : BaseEntity<Guid>
    {
        public Guid BrandModelId { get; set; }
        public BrandModel BrandModel { get; set; }
        public string Number { get; set; }
        public bool IsRent { get; set; }//арендована
        public bool IsRepair { get; set; }//в ремонте
    }
}
