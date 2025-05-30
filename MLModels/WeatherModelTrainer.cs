using Microsoft.ML;


namespace climby.MLModels
{
    public class WeatherModelTrainer
    {
        private readonly string _modelPath;
        private readonly MLContext _mlContext;

        public WeatherModelTrainer(string modelPath = "MLModels/weatherModel.zip")
        {
            _mlContext = new MLContext(seed: 0);
            _modelPath = modelPath;
        }

        public void Train(IDataView trainingData)
        {
            var dataProcessPipeline = _mlContext.Transforms.Concatenate("Features",
                    nameof(WeatherDataInput.Temperature),
                    nameof(WeatherDataInput.Humidity),
                    nameof(WeatherDataInput.Pressure),
                    nameof(WeatherDataInput.WindSpeed))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"));

            var trainer = _mlContext.BinaryClassification.Trainers.FastForest(labelColumnName: nameof(WeatherDataInput.Alert), featureColumnName: "Features");

            var trainingPipeline = dataProcessPipeline.Append(trainer);

            var model = trainingPipeline.Fit(trainingData);

            _mlContext.Model.Save(model, trainingData.Schema, _modelPath);
        }

        public bool PredictAlert(WeatherDataInput input)
        {
            if (!File.Exists(_modelPath))
                throw new FileNotFoundException("Modelo ML não encontrado, treine o modelo primeiro.");

            ITransformer trainedModel = _mlContext.Model.Load(_modelPath, out var modelInputSchema);
            var predEngine = _mlContext.Model.CreatePredictionEngine<WeatherDataInput, WeatherPrediction>(trainedModel);

            var prediction = predEngine.Predict(input);
            return prediction.Alert;
        }
    }
}
