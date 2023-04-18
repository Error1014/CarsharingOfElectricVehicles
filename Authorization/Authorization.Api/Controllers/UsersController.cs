using Authorization.Service.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Filters;
using Infrastructure.HelperModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
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
    }
}
