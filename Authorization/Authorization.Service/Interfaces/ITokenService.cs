using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service.Interfaces
{
    public interface ITokenService
    {
        Task<AuthorizeDTO> GenerateToken(LoginDTO loginDTO);
        Task<UserSession> CheckAuthorize(string? token, string? role);
    }
}
