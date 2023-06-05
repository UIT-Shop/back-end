namespace MyShop.Services.WarehouseService
{
    public interface IWarehouseService
    {
        // Create
        Task<ServiceResponse<Warehouse>> CreateWarehouse(Warehouse warehouse);

        // Read
        Task<ServiceResponse<List<Warehouse>>> GetWarehouses();
        Task<ServiceResponse<Warehouse>> GetWarehouse(int id);

        // Update
        Task<ServiceResponse<Warehouse>> UpdateWarehouse(Warehouse warehouse);

        // Delete
        Task<ServiceResponse<bool>> DeleteWarehouse(int warehouseId);
    }
}
