using Authorization.Repository.Entities;
using Authorization.Service.Interfaces;
using AutoMapper;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Authorization.Api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public UsersController(IUserService userService, IOptions<JwtOptions> jwtOptions)
        {
            _userService = userService;
            _jwtOptions = jwtOptions;
        }
        #region обычные запросы
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var client = await _userService.GetUser(id);
            return Ok(client);
        }
        [RoleAuthorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var list = await _userService.GetUsers(new PageFilter(1, 10));
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(LoginDTO loginDTO)
        {
            UserDTO userDTO = new UserDTO(loginDTO.Login, loginDTO.Password);
            userDTO.RoleId = 3;
            await _userService.AddUser(userDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpPost(nameof(RegistrationOperator))]
        public async Task<IActionResult> RegistrationOperator(LoginDTO loginDTO)
        {
            UserDTO userDTO = new UserDTO(loginDTO.Login, loginDTO.Password);
            userDTO.RoleId = 2;
            await _userService.AddUser(userDTO);
            return Ok();
        }
        [RoleAuthorize("Operator Client")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserDTO userDTO)
        {
            await _userService.UpdateUser(id, userDTO);
            return Ok();
        }
        [RoleAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _userService.RemoveUser(id);
            return Ok();
        }
        #endregion

        #region запросы авторизации
        [HttpPost(nameof(Login))]
        public async Task<IResult> Login(LoginDTO loginDTO)
        {
            var person = await _userService.GetUserByLogin(loginDTO);

            var role = await _userService.GetRole(person.Id);

            var claims = new List<Claim>
            {
                new Claim("Id", person.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,role)
            };
            // создаем JWT-токен
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Value.Key);
            var jwt = new JwtSecurityToken(
                    issuer: _jwtOptions.Value.Issuer,
                    audience: _jwtOptions.Value.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // формируем ответ
            var response = new
            {
                id = person.Id,
                accessToken = encodedJwt,
                username = person.Login,
                role
            };
            return Results.Json(response);
        }

        [HttpPost(nameof(Authorize))]
        public IActionResult Authorize([FromQuery] string? role)
        {
            UserSession userSession = new UserSession();
            var token = ViewData["Authorization"].ToString();
            var roles = role?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (token == "Bearer")
            {
                return Ok(userSession);
            }
            if (token == null)
            {
                throw new Exception("403");
            }
            var jwtToken = DeShifr(token, roles);
            var accountId = Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var myRole = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            bool isAuthorize = false;

            if (roles == null)
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
            if (!isAuthorize) throw new Exception("403");
            return Ok(userSession);
        }

        private JwtSecurityToken DeShifr(string token, List<string> roles)
        {
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
                throw new Exception("401");
            }
            return jwtToken;
        }
        #endregion
    }
}
