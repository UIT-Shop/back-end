namespace MyShop.Services.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly DataContext _context;
        private readonly IAddressService _addressService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehouseService(DataContext context, IHttpContextAccessor httpContextAccessor, IAddressService addressService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _addressService = addressService;
        }

        public async Task<ServiceResponse<Warehouse>> CreateWarehouse(Warehouse warehouse)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin)))
                return new ServiceResponse<Warehouse> { Success = false, Message = "You are not allow to do this action" };
            var dbAddress = (await _addressService.AddAddress(warehouse.Address)).Data;
            warehouse.Address = dbAddress;
            warehouse.AddressId = dbAddress.Id;

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Warehouse> { Data = warehouse };
        }

        public async Task<ServiceResponse<bool>> DeleteWarehouse(int warehouseId)
        {
            var dbWarehouse = await _context.Warehouses.FindAsync(warehouseId);
            if (dbWarehouse == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Warehouse not found."
                };
            }
            _context.Warehouses.Remove(dbWarehouse);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<Warehouse>> GetWarehouse(int id)
        {
            var Warehouse = await _context.Warehouses.FirstOrDefaultAsync(p => p.Id == id);
            if (Warehouse == null)
                return new ServiceResponse<Warehouse>
                {
                    Success = false,
                    Message = "Warehouse not found"
                };
            Warehouse.Address = _addressService.GetAddressById(Warehouse.AddressId).Result.Data;


            return new ServiceResponse<Warehouse>
            {
                Data = Warehouse
            };
        }

        public async Task<ServiceResponse<List<Warehouse>>> GetWarehouses()
        {
            List<Warehouse> Warehouses = await _context.Warehouses.ToListAsync();
            Warehouses.ForEach(w => w.Address = _addressService.GetAddressById(w.AddressId).Result.Data);

            return new ServiceResponse<List<Warehouse>>
            {
                Data = Warehouses
            };
        }

        public async Task<ServiceResponse<Warehouse>> UpdateWarehouse(Warehouse warehouse)
        {
            var dbWarehouse = await _context.Warehouses
                .FirstOrDefaultAsync(p => p.Id == warehouse.Id);

            if (dbWarehouse == null)
            {
                return new ServiceResponse<Warehouse>
                {
                    Success = false,
                    Message = "Warehouse not found."
                };
            }

            var dbAddress = (await _addressService.AddAddress(warehouse.Address)).Data;
            dbWarehouse.Address = dbAddress;
            dbWarehouse.AddressId = dbAddress.Id;

            dbWarehouse.Name = warehouse.Name;
            dbWarehouse.Phone = warehouse.Phone;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Warehouse> { Data = warehouse };
        }
    }
}
