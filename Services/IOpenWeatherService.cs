using climby.DTOs;


namespace climby.Services
{
    public interface IOpenWeatherService
    {
        Task<WeatherInfoDto> GetWeatherByCityAsync(string city);
    }
}
