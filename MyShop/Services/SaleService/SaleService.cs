using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;

namespace MyShop.Services.SaleService
{
    public class SaleService : ISaleService
    {
        private readonly DataContext _context;

        public SaleService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOrUpdateSaleAsync(decimal totalPrice)
        {
            DateTime today = DateTime.Today;
            var dbSale = await _context.Sales.Where(s => s.Date.Year == today.Year && s.Date.Month == today.Month && s.Date.Day == today.Day).FirstAsync();
            if (dbSale == null)
            {
                var sale = new Sale { Date = today, Totals = (float)totalPrice };
                _context.Sales.Add(sale);
            }
            else dbSale.Totals += (float)totalPrice;
            await _context.SaveChangesAsync();
            return true;
        }

        public ServiceResponse<PredictSale> GetPredict()
        {
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "MLModel.zip");
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            MLContext mlContext = new MLContext();

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<Sale>();

            string query = "SELECT Date, CAST(Year as REAL) as Year, CAST(Totals as REAL) as Totals FROM Sales";

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                            connectionString,
                                            query);

            IDataView dataView = loader.Load(dbSource);

            IDataView firstYearData = mlContext.Data.FilterRowsByColumn(dataView, "Year", upperBound: 2022.11);
            IDataView secondYearData = mlContext.Data.FilterRowsByColumn(dataView, "Year", lowerBound: 1);

            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "ForecastedSales",
                inputColumnName: "Totals",
                windowSize: 7,
                seriesLength: 30,
                trainSize: 365,
                horizon: 120,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "LowerBoundSales",
                confidenceUpperBoundColumn: "UpperBoundSales");

            SsaForecastingTransformer forecaster = forecastingPipeline.Fit(firstYearData);

            //Evaluate(secondYearData, forecaster, mlContext);

            var forecastEngine = forecaster.CreateTimeSeriesEngine<Sale, PredictSale>(mlContext);
            try { forecastEngine.CheckPoint(mlContext, modelPath); } catch (Exception ex) { }


            //Forecast(secondYearData, 120, forecastEngine, mlContext);

            PredictSale forecast = forecastEngine.Predict();

            return new ServiceResponse<PredictSale>
            {
                Data = forecast
            };
        }

        static void Evaluate(IDataView testData, ITransformer model, MLContext mlContext)
        {
            // Make predictions
            IDataView predictions = model.Transform(testData);

            // Actual values
            IEnumerable<float> actual =
                mlContext.Data.CreateEnumerable<Sale>(testData, true)
                    .Select(observed => observed.Totals);

            // Predicted values
            IEnumerable<float> forecast =
                mlContext.Data.CreateEnumerable<PredictSale>(predictions, true)
                    .Select(prediction => prediction.ForecastedSales[0]);

            // Calculate error (actual - forecast)
            var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);

            // Get metric averages
            var MAE = metrics.Average(error => Math.Abs(error)); // Mean Absolute Error
            var RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2))); // Root Mean Squared Error

            // Output metrics
            Console.WriteLine("Evaluation Metrics");
            Console.WriteLine("---------------------");
            Console.WriteLine($"Mean Absolute Error: {MAE:F3}");
            Console.WriteLine($"Root Mean Squared Error: {RMSE:F3}\n");
        }

        static void Forecast(IDataView testData, int horizon, TimeSeriesPredictionEngine<Sale, PredictSale> forecaster, MLContext mlContext)
        {

            PredictSale forecast = forecaster.Predict();

            IEnumerable<string> forecastOutput =
                mlContext.Data.CreateEnumerable<Sale>(testData, reuseRowObject: false)
                    .Take(horizon)
                    .Select((Sale rental, int index) =>
                    {
                        string rentalDate = rental.Date.ToShortDateString();
                        float actualSales = rental.Totals;
                        float lowerEstimate = Math.Max(0, forecast.LowerBoundSales[index]);
                        float estimate = forecast.ForecastedSales[index];
                        float upperEstimate = forecast.UpperBoundSales[index];
                        return $"Date: {rentalDate}\n" +
                        $"Actual Sales: {actualSales}\n" +
                        $"Lower Estimate: {lowerEstimate}\n" +
                        $"Forecast: {estimate}\n" +
                        $"Upper Estimate: {upperEstimate}\n";
                    });

            // Output predictions
            Console.WriteLine("Rental Forecast");
            Console.WriteLine("---------------------");
            foreach (var prediction in forecastOutput)
            {
                Console.WriteLine(prediction);
            }
        }

        public async Task<ServiceResponse<List<Sale>>> GetSales()
        {
            var sales = await _context.Sales.OrderByDescending(s => s.Date).Take(120).ToListAsync();
            return new ServiceResponse<List<Sale>>
            {
                Data = sales
            };
        }
    }
}
