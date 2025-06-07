using Microsoft.AspNetCore.Mvc;
using climby.DTOs;

namespace climby.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        [HttpPost("sensor-weather")]
        public IActionResult ReceiveSensorWeather([FromBody] SensorWeatherDataDto sensorData)
        {
            string alertMessage = GenerateAlert(sensorData);

            var result = new WeatherInfoDto
            {
                City = sensorData.City,
                Temperature = sensorData.Temperature,
                Description = "Dados do sensor",
                Icon = "", 
                AlertMessage = alertMessage
            };

            return Ok(result);
        }

        private string GenerateAlert(SensorWeatherDataDto data)
        {
            if (data.RainLevel > 50)
                return "Alerta de chuva forte!";
            else if (data.Temperature > 38)
                return "Alerta de calor extremo!";
            else
                return null;
        }
    }
}
