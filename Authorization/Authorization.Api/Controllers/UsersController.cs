﻿using Authorization.Service.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization.Api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, IOptions<JwtOptions> jwtOptions)
        {
            _userService = userService;
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
        public async Task<IActionResult> RegistrationU(LoginDTO loginDTO)
        {
            UserDTO userDTO = new UserDTO(loginDTO.Login, loginDTO.Password);
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
        public async Task<IActionResult> Update(Guid id, LoginDTO userDTO)
        {
            await _userService.UpdateUser(id, userDTO);
            return Ok();
        }
        [RoleAuthorize("Operator")]
        [HttpPut(nameof(SetClientRole)+"/{id}")]
        public async Task<IActionResult> SetClientRole(Guid id)
        {
            await _userService.SetClientRole(id);
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
    }
}
