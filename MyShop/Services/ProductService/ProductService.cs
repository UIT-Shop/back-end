namespace MyShop.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Product>> CreateProduct(Product product)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin)))
                return new ServiceResponse<Product> { Success = false, Message = "You are not allow to do this action" };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        {
            var dbProduct = await _context.Products.FindAsync(productId);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<ProductSearchResult>> GetAdminProducts(int page)
        {
            var allProducts = await _context.Products
                    .Where(p => !p.Deleted)
                    .ToListAsync();
            var pageResults = 20f;
            var pageCount = Math.Ceiling(allProducts.Count / pageResults);
            var products = await _context.Products
                                .Where(p => !p.Deleted)
                                .Include(p => p.Variants.Where(v => !v.Deleted))
                                .Include(pc => pc.Images)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                    .Where(p => p.Visible && !p.Deleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<int>>> GetListProductIds()
        {
            var products = await _context.Products.ToListAsync();
            List<int> listId = new List<int>();
            foreach (var product in products) { listId.Add(product.Id); }

            return new ServiceResponse<List<int>> { Data = listId };
        }

        public async Task<ServiceResponse<Product>> GetProductById(int productId)
        {
            var response = new ServiceResponse<Product>();
            Product product = _httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin))
                ? await _context.Products
                    .Include(p => p.Category).Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ThenInclude(pv => pv.Color).Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted)
                : await _context.Products
                    .Include(p => p.Category).Include(p => p.Brand)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .ThenInclude(pv => pv.Color).Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted && p.Visible);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exist.";
            }
            else
            {
                response.Data = product;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProducts(List<int> ids)
        {
            List<Product> products = await _context.Products
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .Include(p => p.Images)
                    .Where(p => ids.Contains(p.Id)).ToListAsync();
            return new ServiceResponse<List<Product>> { Data = products };
        }

        public async Task<ServiceResponse<ProductSearchResult>> GetProductsAsync(int page)
        {
            var allProducts = await _context.Products
                    .Where(p => p.Visible && !p.Deleted)
                    .ToListAsync();
            var pageResults = 20f;
            var pageCount = Math.Ceiling(allProducts.Count / pageResults);
            var products = await _context.Products
                                .Where(p => p.Visible && !p.Deleted)
                                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                                .Include(pc => pc.Images)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<ProductSearchResult>> GetProductsByCategory(int categoryId, int page)
        {
            var allProducts = await _context.Products
                    .Where(p => p.CategoryId == categoryId &&
                        p.Visible && !p.Deleted)
                    .ToListAsync();
            var pageResults = 20f;
            var pageCount = Math.Ceiling(allProducts.Count / pageResults);
            var products = await _context.Products
                    .Where(p => p.CategoryId == categoryId &&
                        p.Visible && !p.Deleted)
                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                    .Include(pc => pc.Images)
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> SearchSuggestionProducts(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
        {
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.Deleted)
                                .Include(p => p.Variants)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        {
            var dbProduct = await _context.Products
                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                .ThenInclude(v => v.Color.Images)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Title = product.Title;
            dbProduct.Description = product.Description;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.Visible = product.Visible;

            //var productImages = dbProduct.Variants.SelectMany(v => v.Images).ToList();
            //_context.Images.RemoveRange(productImages);

            //dbProduct.Variants.Images = product.Variants.Images;

            foreach (var variant in product.Variants)
            {
                var dbVariant = await _context.ProductVariants
                    .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
                        v.ProductSize == variant.ProductSize);
                if (dbVariant == null)
                {
                    _context.ProductVariants.Add(variant);
                }
                else
                {
                    dbVariant.ProductSize = variant.ProductSize;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.Visible = variant.Visible;
                    dbVariant.Deleted = variant.Deleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = product };
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.Deleted)
                                .Include(p => p.Variants)
                                .ToListAsync();
        }
    }
}
