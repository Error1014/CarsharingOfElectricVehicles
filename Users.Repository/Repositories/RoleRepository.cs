using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Repository.Entities;
using Users.Repository.Interfaces;

namespace Users.Repository.Repositories
{
    public class RoleRepository: Repository<Role, int>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
