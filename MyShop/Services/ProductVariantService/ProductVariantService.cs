namespace MyShop.Services.ProductVariantService
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly DataContext _context;

        public ProductVariantService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ProductVariant>>> AddProductVariant(ProductVariant productVariant)
        {
            productVariant.Editing = productVariant.IsNew = false;
            _context.ProductVariants.Add(productVariant);
            await _context.SaveChangesAsync();

            return await GetProductVariants(productVariant.ProductId);
        }

        public async Task<ServiceResponse<bool>> DeleteProductVariant(int productVariantId)
        {
            var dbProductVariant = await _context.ProductVariants.FindAsync(productVariantId);
            if (dbProductVariant == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product Variant not found."
                };
            }
            dbProductVariant.Deleted = true;
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<ProductVariant>> GetProductVariant(int productId, int productColorId, string productSize)
        {
            var productVariant = await _context.ProductVariants
                .FirstOrDefaultAsync(p => p.ProductId == productId
                && p.ColorId == productColorId
                && p.ProductSize == productSize);
            return new ServiceResponse<ProductVariant> { Data = productVariant };
        }

        public async Task<ServiceResponse<ProductVariant>> GetProductVariant(int productVariantId)
        {
            var productVariant = await _context.ProductVariants
                .Include(pv => pv.Product).ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == productVariantId);
            return new ServiceResponse<ProductVariant> { Data = productVariant };
        }

        public async Task<ServiceResponse<List<ProductVariant>>> GetProductVariants(int productId)
        {
            var productVariants = await _context.ProductVariants.Where(v => v.ProductId == productId).ToListAsync();
            return new ServiceResponse<List<ProductVariant>> { Data = productVariants };
        }



        public async Task<ServiceResponse<List<ProductVariant>>> UpdateProductVariant(ProductVariant productVariant)
        {
            var dbProductVariant = await _context.ProductVariants.FindAsync(productVariant.Id);
            if (dbProductVariant == null)
            {
                return new ServiceResponse<List<ProductVariant>>
                {
                    Success = false,
                    Message = "Product Variant not found."
                };
            }

            dbProductVariant.Price = productVariant.Price;
            dbProductVariant.OriginalPrice = productVariant.OriginalPrice;
            dbProductVariant.ColorId = productVariant.ColorId;
            dbProductVariant.ProductSize = productVariant.ProductSize;
            dbProductVariant.Quantity = productVariant.Quantity;

            await _context.SaveChangesAsync();

            return await GetProductVariants(productVariant.ProductId);
        }
    }
}
