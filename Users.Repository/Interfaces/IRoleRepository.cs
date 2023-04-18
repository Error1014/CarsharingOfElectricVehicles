using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Repository.Entities;

namespace Users.Repository.Interfaces
{
    public interface IRoleRepository : IRepository<Role, int>
    {
    }
}
