namespace MyShop.Models
{
    public class PredictSale
    {
        public float[] ForecastedSales { get; set; }

        public float[] LowerBoundSales { get; set; }

        public float[] UpperBoundSales { get; set; }
    }
}
