using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace MyShop.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public RatingService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<RatingPerProduct>> AddOrUpdateRating(RatingPerProduct rating, List<Comment> listComment)
        {
            var userId = _authService.GetUserId();
            var averageRating = listComment.Where(c => c.UserId == userId).Average(c => c.Rating);
            var existRating = await _context.Ratings.FirstOrDefaultAsync(r => r.UserId == userId && r.ProductId == rating.ProductId);


            if (existRating == null)
                _context.Ratings.Add(rating);
            else
                existRating.Rating = averageRating;
            await _context.SaveChangesAsync();


            return new ServiceResponse<RatingPerProduct> { Data = rating };
        }



        public void ReTrainData()
        {
            //STEP 1: Create MLContext to be shared across the model creation workflow objects 
            MLContext mlContext = new MLContext();

            //STEP 2: Read the training data which will be used to train the product recommendation model    
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "MLModelRecommend.zip");
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<ModelInput>();

            string query = "SELECT UserId, ProductId, CAST(Rating as REAL) as Rating FROM Ratings";

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                            connectionString,
                                            query);

            IDataView dataView = loader.Load(dbSource);

            ITransformer model = RetrainPipeline(mlContext, dataView);

            mlContext.Model.Save(model, dataView.Schema, "MLModelRecommend.zip");
        }

        /// <summary>
        /// Retrains model using the pipeline generated as part of the training
        /// process. For more information on how to load data, see
        /// aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            string DatasetsRelativePath = @"./Data";

            string TestDataRelativePath = $"{DatasetsRelativePath}/testRecommend.csv";

            string TestDataLocation = GetAbsolutePath(TestDataRelativePath);
            var pipeline = BuildPipeline(mlContext);
            //STEP 5: Train the model fitting to the DataSet
            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = pipeline.Fit(trainData);

            //STEP 6: Evaluate the model performance 
            Console.WriteLine("=============== Evaluating the model ===============");
            IDataView testDataView = mlContext.Data.LoadFromTextFile<ModelInput>(TestDataLocation, hasHeader: true, separatorChar: ',');
            var prediction = model.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Rating", scoreColumnName: "Score");
            Console.WriteLine("The model evaluation metrics RootMeanSquaredError:" + metrics.RootMeanSquaredError);
            Console.WriteLine("=============== End of process ===============");
            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this
        /// function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            //STEP 3: Transform your data by encoding the two features userId and productID. These encoded features will be provided as input
            //        to our MatrixFactorizationTrainer.
            var dataProcessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"ProductId", inputColumnName: @"ProductId")
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"UserId", inputColumnName: @"UserId"));

            //Specify the options for MatrixFactorization trainer            
            MatrixFactorizationTrainer.Options options = new MatrixFactorizationTrainer.Options
            {
                LabelColumnName = @"Rating",
                MatrixColumnIndexColumnName = @"UserId",
                MatrixRowIndexColumnName = @"ProductId",
                ApproximationRank = 139,
                LearningRate = 0.0217322913231859,
                NumberOfIterations = 676,
                Quiet = true
            };

            //STEP 4: Create the training pipeline 
            var trainingPipeLine = dataProcessingPipeline.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            return trainingPipeLine;
        }

        public double GetScore(int predictionproductId)
        {
            //STEP 1: Create MLContext to be shared across the model creation workflow objects 
            MLContext mlContext = new MLContext();

            //STEP 2: Read the training data which will be used to train the product recommendation model    
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "MLModelRecommend.zip");

            ITransformer model = mlContext.Model.Load(modelPath, out var _);
            //STEP 3:  Try/test a single prediction by predicting a single product rating for a specific user
            int userId = _authService.GetUserId();
            var predictionengine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            /* Make a single product rating prediction, the scores are for a particular user and will range from 1 - 5. 
               The higher the score the higher the likelyhood of a user liking a particular product.
               You can recommend a product to a user if say rating > 3.5.*/
            ModelOutput productratingprediction = predictionengine.Predict(
                new ModelInput()
                {
                    //Example rating prediction 
                    UserId = userId,
                    ProductId = predictionproductId
                }
            );

            return Math.Round(productratingprediction.Score, 2);
        }

        public List<RecommendOutput> GetRecommend(List<int> productIds)
        {
            int maxItem = 10;
            List<RecommendOutput> recommendOutputs = new List<RecommendOutput>();
            //STEP 1: Create MLContext to be shared across the model creation workflow objects 
            MLContext mlContext = new MLContext();

            //STEP 2: Read the training data which will be used to train the product recommendation model    
            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "MLModelRecommend.zip");

            ITransformer model = mlContext.Model.Load(modelPath, out var _);
            //STEP 3:  Try/test a single prediction by predicting a single product rating for a specific user
            int userId = _authService.GetUserId();
            var predictionengine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);
            /* Make a single product rating prediction, the scores are for a particular user and will range from 1 - 5. 
               The higher the score the higher the likelyhood of a user liking a particular product.
               You can recommend a product to a user if say rating > 3.5.*/
            foreach (var productId in productIds)
            {
                ModelOutput productratingprediction = predictionengine.Predict(
                    new ModelInput()
                    {
                        //Example rating prediction 
                        UserId = userId,
                        ProductId = productId
                    }
                );
                RecommendOutput recommendOutput = new RecommendOutput() { ProductId = productId, Score = productratingprediction.Score };
                recommendOutputs.Add(recommendOutput);
            }
            recommendOutputs = recommendOutputs.OrderByDescending(r => r.Score).Take(maxItem).ToList();
            return recommendOutputs;

        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        /// <summary>
        /// model input class for MLModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"UserId")]
            [LoadColumn(0)]
            public int UserId { get; set; }

            [ColumnName(@"Rating")]
            [LoadColumn(2)]
            public float Rating { get; set; }

            [ColumnName(@"ProductId")]
            [LoadColumn(1)]
            public int ProductId { get; set; }
        }
        #endregion

        /// <summary>
        /// model output class for MLModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Score")]
            public float Score { get; set; }
        }
        #endregion

        public class RecommendOutput
        {
            public int ProductId { get; set; }
            public double Score { get; set; }
        }
    }
}
