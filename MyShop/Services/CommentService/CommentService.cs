using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace MyShop.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CommentService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<List<Comment>>> GetComments(int productId)
        {
            var comments = await _context.Comments
                .Include(c => c.User).Include(c => c.ProductVariant)
                .Where(c => c.ProductVariant.ProductId == productId)
                .ToListAsync();
            return new ServiceResponse<List<Comment>>
            {
                Data = comments
            };
        }

        public async Task<ServiceResponse<Comment>> AddComment(Comment comment)
        {
            var productVariant = await _context.ProductVariants.FindAsync(comment.ProductVariantId);
            if (productVariant == null)
                return new ServiceResponse<Comment>
                {
                    Data = null,
                    Success = false,
                    Message = "Variant not found"
                };
            comment.ProductId = productVariant.ProductId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Comment> { Data = comment };
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

            string query = "SELECT UserId, ProductId, CAST(Rating as REAL) as Rating FROM Comments";

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
            var pipeline = BuildPipeline(mlContext);
            //STEP 5: Train the model fitting to the DataSet
            Console.WriteLine("=============== Training the model ===============");
            ITransformer model = pipeline.Fit(trainData);

            //STEP 6: Evaluate the model performance 
            //Console.WriteLine("=============== Evaluating the model ===============");
            //IDataView testDataView = mlContext.Data.LoadFromTextFile<ModelInput>(TestDataLocation, hasHeader: true, separatorChar: ',');
            //var prediction = model.Transform(testDataView);
            //var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            //Console.WriteLine("The model evaluation metrics RootMeanSquaredError:" + metrics.RootMeanSquaredError);            
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
                ApproximationRank = 10,
                LearningRate = 0.128927493810545,
                NumberOfIterations = 923,
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
                Console.WriteLine($"{recommendOutput.ProductId}\t\t\t{recommendOutput.Score}");
                recommendOutputs.Add(recommendOutput);
            }
            recommendOutputs = recommendOutputs.OrderByDescending(r => r.Score).Take(maxItem).ToList();
            return recommendOutputs;

        }

        /// <summary>
        /// model input class for MLModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"UserId")]
            public int UserId { get; set; }

            [ColumnName(@"Rating")]
            public float Rating { get; set; }

            [ColumnName(@"ProductId")]
            public int ProductId { get; set; }
        }
        #endregion
        //eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidXNlcjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ1c2VyMUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTY3MTQ4NjQ2Nn0.i0FkJeUQ3x-LrZOB-XgnWZzh07WsX_1hhnW_A6kHGHOIvrNU3LaZzF9atDFw08X8lSfn46kEgf4jeCimWgC6TQ

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
