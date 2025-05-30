using Microsoft.ML.Data;

namespace climby.MLModels
{
    public class WeatherPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Alert;
    }
}
