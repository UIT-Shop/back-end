﻿namespace MyShop.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            foreach (var item in cartItems)
            {
                var productVariant = await _context.ProductVariants
                    .Include(pv => pv.Product)
                    .Include(pv => pv.Color)
                    .ThenInclude(c => c.Images)
                    .FirstOrDefaultAsync(pv => pv.Id == item.ProductVariantId);

                if (productVariant == null)
                {
                    continue;
                }

                var product = await _context.Products
                    .Where(p => p.Id == productVariant.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                }

                var imageUrl = "0";
                if (productVariant.Color.Images.Count() != 0)
                    imageUrl = productVariant.Color.Images.FirstOrDefault(i => i.ProductId == productVariant.ProductId && i.ColorId == productVariant.ColorId).Url;

                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    ProductVariantId = productVariant.Id,
                    Title = product.Title,
                    ImageUrl = imageUrl,
                    Price = productVariant.Price,
                    ProductSize = productVariant.ProductSize ?? "XL",
                    Color = productVariant.Color.Name,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProducts();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(int? userId = null)
        {
            userId ??= _authService.GetUserId();

            return await GetCartProducts(await _context.CartItems
                .Where(ci => ci.UserId == userId).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductVariantId == cartItem.ProductVariantId
                && ci.UserId == cartItem.UserId);
            if (sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductVariantId == cartItem.ProductVariantId
                && ci.UserId == cartItem.UserId);
            if (sameItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            sameItem.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productVariantId)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductVariantId == productVariantId
                && ci.UserId == _authService.GetUserId());
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
