using Authorization.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Api.Controllers
{

    public class AutorizationController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public AutorizationController(IUserService userService, IOptions<JwtOptions> jwtOptions)
        {
            _userService = userService;
            _jwtOptions = jwtOptions;
        }

        #region запросы авторизации
        [HttpPost(nameof(Login))]
        public async Task<IResult> Login(LoginDTO loginDTO)
        {
            var person = await _userService.GetUserByLogin(loginDTO);

            var role = await _userService.GetRole(person.Id);
            if (role == null) role = string.Empty;
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
                return new StatusCodeResult(401);
            }
            if (token.IsNullOrEmpty())
            {
                return new StatusCodeResult(401);
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
                return new StatusCodeResult(401);
            }
            var accountId = Guid.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var myRole = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            if (myRole.IsNullOrEmpty())
            {
                return new StatusCodeResult(401);
            }
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
            if (!isAuthorize) return new StatusCodeResult(403);
            return Ok(userSession);
        }

        #endregion
    }
}
