using Newtonsoft.Json.Linq;
using climby.DTOs;
using climby.Repositories;
using System.Text;

namespace climby.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public OpenWeatherService(HttpClient httpClient, IConfiguration configuration, IUserRepository userRepository)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<WeatherInfoDto> GetWeatherByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("Cidade inválida.");

            var apiKey = _configuration["OpenWeather:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=pt_br";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JObject.Parse(content);

            var temperature = weatherData["main"]["temp"].Value<decimal>();
            var description = weatherData["weather"][0]["description"].Value<string>();
            var icon = weatherData["weather"][0]["icon"].Value<string>();
            var humidity = weatherData["main"]["humidity"].Value<float>();
            var pressure = weatherData["main"]["pressure"].Value<float>();
            var windSpeed = weatherData["wind"]["speed"].Value<float>();

            string alertMessage = null;
            bool shouldAlert = false;

            if (description.Contains("chuva") || description.Contains("tempestade"))
                alertMessage = "Alerta de chuva forte. Se estiver em área de risco, procure abrigo!";
            else if (temperature >= 35)
                alertMessage = "Alerta de calor extremo. Beba bastante água e evite sair no sol!";

            string csvLine = $"{temperature},{humidity},{windSpeed},{pressure},\"{description.Replace("\"", "")}\",{(shouldAlert ? 1 : 0)}";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "ML");
            Directory.CreateDirectory(folderPath);
            string csvPath = Path.Combine(folderPath, "weather-dataset.csv");

            bool fileExists = File.Exists(csvPath);

            using (var writer = new StreamWriter(csvPath, append: true, encoding: Encoding.UTF8))
            {
                if (!fileExists)
                {
                    writer.WriteLine("Temperature,Humidity,WindSpeed,Pressure,Description,ShouldAlert");
                }

                writer.WriteLine(csvLine);
            }

            return new WeatherInfoDto
            {
                City = city,
                Temperature = temperature,
                Description = description,
                Icon = icon,
                AlertMessage = alertMessage
            };
        }
    }
}
