using Microsoft.AspNetCore.Mvc;
using climby.Services;

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
        public async Task<IActionResult> GetSheltersByCity([FromQuery] string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest(new { message = "Cidade é obrigatória." });
            }

            var shelters = await _shelterService.GetAllByCityAsync(city);
            return Ok(shelters);
        }
    }
}
