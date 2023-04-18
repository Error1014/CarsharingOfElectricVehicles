using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients.Repository.Entities
{
    public class Client : BaseEntity<Guid>
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public decimal? Balance { get; set; }

    }
}
