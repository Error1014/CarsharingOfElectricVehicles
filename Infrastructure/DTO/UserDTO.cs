using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }

        public UserDTO(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
