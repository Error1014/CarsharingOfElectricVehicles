using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class AuthorizeDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public AuthorizeDTO() { }

        public AuthorizeDTO(Guid userId, string token, string emial, string role) 
        {
            UserId = userId;
            Token = token;
            Email = emial;
            Role = role;
        }
    }
}
