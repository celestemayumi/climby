using Microsoft.AspNetCore.Mvc;
using climby.Services;

namespace climby.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IOpenWeatherService _weatherService;

        public WeatherController(IOpenWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetWeatherToday([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("A cidade precisa ser informada.");

            try
            {
                var weatherInfo = await _weatherService.GetWeatherByCityAsync(city);
                return Ok(weatherInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar clima: {ex.Message}");
            }
        }
    }
}
