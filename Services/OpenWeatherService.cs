
using Newtonsoft.Json.Linq;
using climby.DTOs;
using climby.Repositories;
using climby.Services;

namespace SeuProjeto.Services
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
        
        public async Task<WeatherInfoDto> GetWeatherByFirebaseUidAsync(string firebaseUid)
        {
            var user = await _userRepository.GetByFirebaseUidAsync(firebaseUid);

            if (user == null || string.IsNullOrWhiteSpace(user.City))
                throw new Exception("Usuário não encontrado ou cidade não definida.");

            var city = user.City;
            var apiKey = _configuration["OpenWeather:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=pt_br";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JObject.Parse(content);

            var temperature = weatherData["main"]["temp"].Value<decimal>();
            var description = weatherData["weather"][0]["description"].Value<string>();
            var icon = weatherData["weather"][0]["icon"].Value<string>();

            string alertMessage = null;

            if (description.Contains("chuva") || description.Contains("tempestade"))
                alertMessage = "Alerta de chuva forte. Se estiver em área de risco, procure abrigo!";
            else if (temperature >= 35)
                alertMessage = "Alerta de calor extremo. Beba bastante água e evite sair no sol!";


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
