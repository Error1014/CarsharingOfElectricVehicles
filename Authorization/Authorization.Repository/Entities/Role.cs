using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Repository.Entities
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
