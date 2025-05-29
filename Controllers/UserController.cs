using climby.DTOs;
using climby.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace climby.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyUser()
        {
            var uid = User.FindFirstValue("uid");
            if (uid == null)
                return Unauthorized("Usuário não autenticado.");

            var userDto = await _userService.GetUserByFirebaseUidAsync(uid);
            if (userDto == null)
                return NotFound("Usuário não encontrado.");

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoDto dto)
        {
            var uid = User.FindFirstValue("uid");
            if (uid == null)
                return Unauthorized("Usuário não autenticado.");

            try
            {
                var createdUser = await _userService.CreateUserAsync(uid, dto);
                return CreatedAtAction(nameof(GetMyUser), new { }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserInfoDto dto)
        {
            var uid = User.FindFirstValue("uid");
            if (uid == null)
                return Unauthorized("Usuário não autenticado.");

            var updatedUser = await _userService.UpdateUserAsync(uid, dto);
            if (updatedUser == null)
                return NotFound("Usuário não encontrado.");

            return Ok(updatedUser);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            var uid = User.FindFirstValue("uid");
            if (uid == null)
                return Unauthorized("Usuário não autenticado.");

            var result = await _userService.DeleteUserAsync(uid);
            if (!result)
                return NotFound("Usuário não encontrado.");

            return NoContent();
        }
    }
}
