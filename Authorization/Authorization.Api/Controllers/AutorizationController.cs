using Authorization.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly ITokenService _tokenService;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public AutorizationController( IUserService userService,ITokenService tokenService, IOptions<JwtOptions> jwtOptions)
        {
            _userService = userService;
            _tokenService = tokenService;
            _jwtOptions = jwtOptions;
        }

        #region запросы авторизации
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _tokenService.GenerateToken(loginDTO);
            return Ok(result);
        }


        [HttpPost(nameof(Authorize))]
        public async Task<IActionResult> Authorize([FromQuery] string? role)
        {
            var token = ViewData["Authorization"].ToString();
            var result = await _tokenService.CheckAuthorize(token, role);
            return Ok(result);
        }

        [HttpGet(nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            await _userService.ConfirmationEmail(userId, token);
            return Ok("Почта подтверждена");
        }

        #endregion
    }
}
