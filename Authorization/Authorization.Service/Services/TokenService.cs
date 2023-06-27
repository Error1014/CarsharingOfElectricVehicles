using Authorization.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Service.Services
{
    public class TokenService: ITokenService
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public TokenService(IUserService userService, IOptions<JwtOptions> jwtOptions)
        {
            _userService = userService;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthorizeDTO> GenerateToken(LoginDTO loginDTO)
        {
            var person = await _userService.GetUserByLogin(loginDTO);

            var role = await _userService.GetRole(person.Id);
            if (role == null) role = string.Empty;
            var claims = new List<Claim>
            {
                new Claim("Id", person.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,role),
                new Claim("IsEmailСonfirmed",person.IsEmailСonfirmed.ToString())
            };
            // создаем JWT-токен
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key);
            var jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Value.Issuer,
                    audience: _jwtOptions.Value.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new AuthorizeDTO(person.Id, encodedJwt, person.Login, role);
            return response;
        }

        public async Task<UserSession> CheckAuthorize(string? token, string? role)
        {
            UserSession userSession = new UserSession();
            var roles = role?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (token == "Bearer")
            {
                throw new UnauthorizedException("Вы не авторизованы");
            }
            if (token.IsNullOrEmpty())
            {
                throw new UnauthorizedException("Вы не авторизованы");
            }
            var jwtToken = new JwtSecurityToken();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _jwtOptions.Value.Issuer,
                    ValidAudience = _jwtOptions.Value.Audience
                }, out SecurityToken validatedToken);
                jwtToken = (JwtSecurityToken)validatedToken;
            }
            catch
            {
                throw new UnauthorizedException("Вы не авторизованы");
            }
            var accountId = Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var myRole = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            var isEmailComfired = bool.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "IsEmailСonfirmed").Value);
            if (myRole.IsNullOrEmpty())
            {
                throw new UnauthorizedException("Вы не авторизованы");
            }
            if (!isEmailComfired)
            {
                throw new UnauthorizedException("Вы не подтвердили почту");
            }
            bool isAuthorize = false;
            if (roles.IsNullOrEmpty())
            {
                userSession.Role = myRole;
                userSession.UserId = accountId;
                isAuthorize = true;
            }
            else
            {
                foreach (var item in roles)
                {
                    if (item == myRole)
                    {
                        userSession.Role = myRole;
                        userSession.UserId = accountId;
                        isAuthorize = true;
                        break;
                    }
                }
            }

            if (!isAuthorize) throw new ForbiddenException("Доступ запрещён");
            return userSession;
        }
    }
}
