using climby.DTOs;
using climby.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
            {
                return Unauthorized("Email ou senha inválidos.");
            }

            var response = new UserDto
            {
                Id = user.ID,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                City = user.City
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userDto = await _userService.GetUserByIdAsync(id);
            if (userDto == null)
                return NotFound("Usuário não encontrado.");

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoDto dto)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserInfoDto dto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, dto);
                if (updatedUser == null)
                    return NotFound("Usuário não encontrado.");

                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound("Usuário não encontrado.");

            return NoContent();
        }
    }
}
