using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        [Authorize]
        public async Task<IActionResult> GetWeatherToday()
        {

            var firebaseUid = User.FindFirstValue("uid");
            Console.WriteLine("Firebase UID: " + firebaseUid);


            if (string.IsNullOrEmpty(firebaseUid))
                return Unauthorized("Token Firebase inválido");

            var weatherInfo = await _weatherService.GetWeatherByFirebaseUidAsync(firebaseUid);
            return Ok(weatherInfo);
        }
    }
}
