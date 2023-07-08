namespace MyShop.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrder(string? name, string? phone, Address? address);

        Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
        Task<ServiceResponse<OrdersAdminResponse>> GetOrdersAdmin(int page, Status status);
        Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);

        Task<ServiceResponse<bool>> UpdateStatus(int order, Status status);
        Task<ServiceResponse<bool>> UpdateIsComment(int orderId, int productVariantId);
    }
}
