using Microsoft.ML.Data;

namespace climby.MLModels
{
    public class WeatherDataInput
    {
        [LoadColumn(0)]
        public float Temperature;

        [LoadColumn(1)]
        public float Humidity;

        [LoadColumn(2)]
        public float Pressure;

        [LoadColumn(3)]
        public float WindSpeed;

        [LoadColumn(4)]
        public bool Alert;
    }
}
