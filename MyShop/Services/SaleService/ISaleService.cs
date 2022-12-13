namespace MyShop.Services.SaleService
{
    public interface ISaleService
    {
        Task<bool> CreateOrUpdateSaleAsync(decimal totalPrice);

        ServiceResponse<PredictSale> GetPredict();
        Task<ServiceResponse<List<Sale>>> GetSales();
    }
}
