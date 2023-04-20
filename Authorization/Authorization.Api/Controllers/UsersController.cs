﻿using Authorization.Repository.Entities;
using Authorization.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        [HttpGet]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _userService.GetUser(id);
            return Ok(client);
        }
        [HttpGet(nameof(GetClients))]
        public async Task<IActionResult> GetClients()
        {
            var list = await _userService.GetUsers(new PageFilter(1, 10));
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserDTO userDTO)
        {
            await _userService.AddUser(userDTO);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateClient(UserDTO userDTO)
        {
            await _userService.UpdateUser(userDTO);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveClient(Guid Id)
        {
            await _userService.RemoveUser(Id);
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
        public async Task<ActionResult<UserSession>> Authorize([FromQuery] string? role)
        {

            UserSession userSession = new UserSession();
            var token = ViewData["Authorization"].ToString();
            if (role == null) return Ok(JsonSerializer.Serialize(userSession));
            var roles = role.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (token != "Bearer")
            {
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
                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var accountId = Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                    var myRole = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
                    bool isAuthorize = false;
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
                    if (!isAuthorize) throw new Exception("403");// StatusCode(403);
                }
                catch
                {
                    throw new Exception("401");// StatusCode(401);
                }
            }
            else
            {

                throw new Exception("401");// StatusCode(401);
            }
            return Ok(userSession);
        }
        #endregion
    }
}