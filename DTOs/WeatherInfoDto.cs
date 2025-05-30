namespace climby.DTOs
{
    public class WeatherInfoDto
    {
        public string City { get; set; }
        public decimal Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string AlertMessage { get; set; } 
    }
}
