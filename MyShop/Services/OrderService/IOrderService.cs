namespace MyShop.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder(string? name, string? phone, Address? address);

        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrdersAdmin();
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);

        Task<ServiceResponse<bool>> UpdateStatus(int order, Status status);
    }
}
