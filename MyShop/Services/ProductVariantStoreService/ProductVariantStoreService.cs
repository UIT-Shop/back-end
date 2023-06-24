namespace MyShop.Services.ProductVariantStoreService
{
    public class ProductVariantStoreService : IProductVariantStoreService
    {
        private readonly DataContext _context;
        private readonly IProductVariantService _productVariantService;

        public ProductVariantStoreService(DataContext context, IProductVariantService productVariantService)
        {
            _context = context;
            _productVariantService = productVariantService;
        }

        public async Task<ServiceResponse<bool>> AddProductVariantStore(ProductVariantStoreInput productVariantStoreInput)
        {
            var variant = _productVariantService.GetProductVariant(productVariantStoreInput.ProductId, productVariantStoreInput.ColorId, productVariantStoreInput.Size).Result.Data;
            ProductVariantStore productVariantStore = await _context.ProductVariantStores
                    .Where(p => p.WarehouseId == productVariantStoreInput.WarehouseId && p.ProductVariantId == variant.Id && p.LotCode == productVariantStoreInput.LotCode)
                    .FirstOrDefaultAsync();
            variant.Quantity += productVariantStoreInput.Quantity;
            _productVariantService.UpdateProductVariant(variant);

            if (productVariantStore != null)
            {
                productVariantStore.Quantity += productVariantStoreInput.Quantity;
                productVariantStore.Stock = variant.Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                if (productVariantStoreInput.Quantity < 0)
                {
                    productVariantStore = await _context.ProductVariantStores
                        .Where(p => p.WarehouseId == productVariantStoreInput.WarehouseId && p.ProductVariantId == variant.Id)
                        .OrderBy(p => p.LotCode)
                        .FirstOrDefaultAsync();
                    if (productVariantStore == null)
                        return new ServiceResponse<bool>
                        {
                            Success = false,
                            Data = false,
                            Message = "Không tồn tại sản phẩm " + variant.Id.ToString() + " trong kho hàng"
                        };
                }
                productVariantStore = new ProductVariantStore
                {
                    ProductVariantId = variant.Id,
                    WarehouseId = productVariantStoreInput.WarehouseId,
                    BuyPrice = productVariantStoreInput.BuyPrice,
                    Quantity = productVariantStoreInput.Quantity,
                    Stock = variant.Quantity,
                    DateInput = productVariantStoreInput.DateInput,
                    LotCode = productVariantStoreInput.LotCode,
                    Note = productVariantStoreInput.Note
                };
                _context.ProductVariantStores.Add(productVariantStore);
                await _context.SaveChangesAsync();
            }

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> MoveProductVariantStore(ProductVariantStoreInput productVariantStoreInput)
        {
            var variant = _productVariantService.GetProductVariant(productVariantStoreInput.ProductId, productVariantStoreInput.ColorId, productVariantStoreInput.Size).Result.Data;

            ProductVariantStore productVariantStore = await _context.ProductVariantStores
                .Where(p => p.WarehouseId == productVariantStoreInput.WarehouseFromId && p.ProductVariantId == variant.Id)
                .OrderBy(p => p.LotCode)
                .FirstOrDefaultAsync();
            if (productVariantStore != null)
            {
                productVariantStore = new ProductVariantStore
                {
                    ProductVariantId = variant.Id,
                    WarehouseId = productVariantStoreInput.WarehouseId,
                    BuyPrice = productVariantStoreInput.BuyPrice,
                    Quantity = -productVariantStoreInput.Quantity,
                    Stock = variant.Quantity,
                    DateInput = productVariantStoreInput.DateInput,
                    LotCode = productVariantStoreInput.LotCode,
                    Note = productVariantStoreInput.Note
                };
                _context.ProductVariantStores.Add(productVariantStore);
                await _context.SaveChangesAsync();
            }
            else
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Không tồn tại sản phẩm " + variant.Id.ToString() + " trong kho hàng"
                };

            productVariantStore = await _context.ProductVariantStores
                   .Where(p => p.WarehouseId == productVariantStoreInput.WarehouseId && p.ProductVariantId == variant.Id && p.LotCode == productVariantStoreInput.LotCode)
                   .FirstOrDefaultAsync();
            if (productVariantStore != null)
            {
                productVariantStore.Quantity += productVariantStoreInput.Quantity;
                productVariantStore.Stock += productVariantStoreInput.Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                productVariantStore = new ProductVariantStore
                {
                    ProductVariantId = variant.Id,
                    WarehouseId = productVariantStoreInput.WarehouseId,
                    BuyPrice = productVariantStoreInput.BuyPrice,
                    Quantity = productVariantStoreInput.Quantity,
                    Stock = variant.Quantity,
                    DateInput = productVariantStoreInput.DateInput,
                    LotCode = productVariantStoreInput.LotCode,
                    Note = productVariantStoreInput.Note
                };
                _context.ProductVariantStores.Add(productVariantStore);
                await _context.SaveChangesAsync();
            }
            return new ServiceResponse<bool> { Data = true };

        }

        public async Task<ServiceResponse<bool>> DeleteProductVariantStore(int productVariantStoreId)
        {
            var dbProductVariantStore = await _context.ProductVariantStores.FindAsync(productVariantStoreId);
            if (dbProductVariantStore == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product variant store not found."
                };
            }
            dbProductVariantStore.Deleted = true;
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<ProductVariantStore>> GetProductVariantStore(int productVariantStoreId)
        {
            var productVariantStore = await _context.ProductVariantStores
                .FirstOrDefaultAsync(p => p.Id == productVariantStoreId);
            return new ServiceResponse<ProductVariantStore> { Data = productVariantStore };
        }

        public async Task<ServiceResponse<List<ProductVariantStoreOutput>>> GetProductVariantStoresByProduct(int productId, DateTime monthYear)
        {
            var firstDayOfMonth = new DateTime(monthYear.Year, monthYear.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var productVariantStores = await _context.ProductVariantStores
                .Include(p => p.Warehouse)
                .Include(p => p.ProductVariant)
                .ThenInclude(pv => pv.Color)
                .Where(p => p.ProductVariant.ProductId == productId && p.DateInput <= lastDayOfMonth && p.DateInput >= firstDayOfMonth).ToListAsync();
            var response = new ServiceResponse<List<ProductVariantStoreOutput>>();
            var productVariantStoreOutput = new List<ProductVariantStoreOutput>();
            var data = new List<ProductVariantStoreOutput>();

            productVariantStores.ForEach(ps => productVariantStoreOutput.Add(new ProductVariantStoreOutput
            {
                WarehouseId = ps.WarehouseId,
                WarehouseName = ps.Warehouse.Name,
                Color = ps.ProductVariant.Color.Name,
                Size = ps.ProductVariant.ProductSize,
                QuantityIn = ps.Quantity > 0 ? ps.Quantity : 0,
                QuantityOut = ps.Quantity < 0 ? ps.Quantity : 0,
            }));
            data = productVariantStoreOutput.GroupBy(key => new
            {
                key.Color,
                key.Size,
                key.WarehouseId,
            })
            .Select(
                ps => new ProductVariantStoreOutput
                {
                    WarehouseId = ps.Key.WarehouseId,
                    WarehouseName = ps.First().WarehouseName,
                    Color = ps.Key.Color,
                    Size = ps.Key.Size,
                    QuantityIn = ps.Sum(qIn => qIn.QuantityIn),
                    QuantityOut = ps.Sum(qOut => qOut.QuantityOut)
                })
            .ToList();

            response.Data = data;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<List<ProductVariantStoreOutput>>> GetProductVariantStoresByWarehouse(int warehouseId, DateTime monthYear)
        {
            var firstDayOfMonth = new DateTime(monthYear.Year, monthYear.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var productVariantStores = await _context.ProductVariantStores
                .Include(p => p.ProductVariant)
                .ThenInclude(pv => pv.Color)
                .Include(p => p.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .Where(p => p.WarehouseId == warehouseId && p.DateInput <= lastDayOfMonth && p.DateInput >= firstDayOfMonth).ToListAsync();
            var response = new ServiceResponse<List<ProductVariantStoreOutput>>();
            var productVariantStoreOutput = new List<ProductVariantStoreOutput>();
            var data = new List<ProductVariantStoreOutput>();

            productVariantStores.ForEach(ps => productVariantStoreOutput.Add(new ProductVariantStoreOutput
            {
                ProductId = ps.ProductVariant.ProductId,
                ProductName = ps.ProductVariant.Product.Title,
                Color = ps.ProductVariant.Color.Name,
                Size = ps.ProductVariant.ProductSize,
                QuantityIn = ps.Quantity > 0 ? ps.Quantity : 0,
                QuantityOut = ps.Quantity < 0 ? ps.Quantity : 0,
            }));

            data = productVariantStoreOutput.GroupBy(key => new
            {
                key.Color,
                key.Size,
                key.ProductId,
            })
            .Select(
                ps => new ProductVariantStoreOutput
                {
                    ProductId = ps.Key.ProductId,
                    ProductName = ps.First().ProductName,
                    Color = ps.Key.Color,
                    Size = ps.Key.Size,
                    QuantityIn = ps.Sum(qIn => qIn.QuantityIn),
                    QuantityOut = ps.Sum(qOut => qOut.QuantityOut)
                })
            .ToList();

            response.Data = data;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateProductVariantStore(ProductVariantStore productVariantStore)
        {
            var dbProductVariantStore = await _context.ProductVariantStores.Include(p => p.ProductVariant).Where(v => v.ProductVariant.Id == productVariantStore.Id).FirstAsync();
            if (dbProductVariantStore == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Product variant not found."
                };
            }

            dbProductVariantStore.BuyPrice = productVariantStore.BuyPrice;
            dbProductVariantStore.Quantity = productVariantStore.Quantity;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Success = true
            };
        }
    }
}
