using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Attributes
{
    public class RoleAuthorizeAttribute : Attribute
    {
        public string Roles { get; }
        public RoleAuthorizeAttribute() { }
        public RoleAuthorizeAttribute(string roles) => Roles = roles;
    }
}
