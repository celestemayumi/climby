using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using climby.Services;
using System.Security.Claims;

namespace climby.Controllers
{
    [ApiController]
    [Route("api/shelters")]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService _shelterService;

        public ShelterController(IShelterService shelterService)
        {
            _shelterService = shelterService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSheltersByCity()
        {
            var city = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;

            if (string.IsNullOrEmpty(city))
            {
                return BadRequest(new { message = "Cidade não encontrada no token." });
            }

            var shelters = await _shelterService.GetAllByCityAsync(city);
            return Ok(shelters);
        }
    }
}
