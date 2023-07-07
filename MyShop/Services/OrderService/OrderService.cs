namespace MyShop.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IAddressService _addressService;
        private readonly IUserService _userService;
        private readonly ISaleService _saleService;

        public OrderService(DataContext context, ICartService cartService, IAuthService authService, IAddressService addressService, IUserService userService, ISaleService saleService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
            _addressService = addressService;
            _userService = userService;
            _saleService = saleService;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var order = await _context.Orders
               .Include(o => o.OrderItems)
               .ThenInclude(oi => oi.Product).ThenInclude(p => p.Images).ThenInclude(i => i.Color)
               .Include(o => o.User)
               .Include(o => o.Address).ThenInclude(a => a.Ward).ThenInclude(w => w.District).ThenInclude(d => d.Province)
               .Where(o => o.Id == orderId)
               .OrderByDescending(o => o.OrderDate)
               .FirstOrDefaultAsync();

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                User = order.User,
                Name = order.Name,
                Phone = order.Phone,
                Status = order.Status,
                Address = order.Address,
                Products = new List<OrderDetailsProductResponse>()
            };

            order.OrderItems.ForEach(item =>
            {
                var imageUrl = "0";
                if (item.Product.Images.Count() != 0)
                    imageUrl = item.Product.Images.FirstOrDefault(i => i.ProductId == item.ProductId && i.Color != null && i.Color.Name == item.ProductColor).Url;

                orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
                {
                    ProductId = item.ProductId,
                    ImageUrl = imageUrl,
                    ProductSize = item.ProductSize,
                    ProductColor = item.ProductColor,
                    ProductVariantID = item.ProductVariantId,
                    Quantity = item.Quantity,
                    Title = item.Product.Title,
                    TotalPrice = item.TotalPrice
                });
            });


            response.Data = orderDetailsResponse;

            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(opi => opi.Images)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Product.Title} and" +
                    $" {o.OrderItems.Count - 1} more..." :
                    o.OrderItems.First().Product.Title,
                ProductImageUrl = o.OrderItems.First().Product.Images.First().Url,
                Status = o.Status,
            }));

            response.Data = orderResponse;
            response.Success = true;
            return response;
        }

        public async Task<ServiceResponse<OrdersAdminResponse>> GetOrdersAdmin(int page, Status status)
        {
            var allOrders = _context.Orders.Where(o => o.Status == status).Count();
            var pageResults = 20f;
            var pageCount = Math.Ceiling(allOrders / pageResults);
            if (pageCount == 0) pageCount = 1;
            DateTime date = new DateTime(2023, 1, 1);
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Status == status && o.OrderDate > date)
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var orderOverviews = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderOverviews.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Product = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Product.Title} và" +
                    $" {o.OrderItems.Count - 1} sản phẩm khác." :
                    o.OrderItems.First().Product.Title,
                ProductImageUrl = "",
                Status = o.Status,
            }));

            var response = new ServiceResponse<OrdersAdminResponse>
            {
                Data = new OrdersAdminResponse
                {
                    OrderOverviews = orderOverviews,
                    CurrentPage = page,
                    Pages = (int)pageCount
                },
                Success = true
            };
            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder(string? name, string? phone, Address? address)
        {
            var userId = _authService.GetUserId();
            var user = (await _userService.GetUserInfo(userId)).Data;
            if (name == null || name.Trim().Length == 0) name = user.Name;
            if (phone == null || phone.Trim().Length == 0) phone = user.Phone;
            if (address == null)
            {
                if (user.Address == null) return new ServiceResponse<bool> { Data = false, Success = false, Message = "Please add address" };
                address = user.Address;
            }
            else address = (await _addressService.AddAddress(address)).Data;
            var products = (await _cartService.GetDbCartProducts(userId)).Data;
            if (products == null || products.Count == 0) return new ServiceResponse<bool> { Data = false, Success = false, Message = "There is no cart item" };
            decimal totalPrice = 0;
            products.ForEach(product => totalPrice += product.Price * product.Quantity);

            var orderItems = new List<OrderItem>();
            products.ForEach(product => orderItems.Add(new OrderItem
            {
                ProductId = product.ProductId,
                ProductSize = product.ProductSize,
                ProductColor = product.Color,
                ProductVariantId = product.ProductVariantId,
                Quantity = product.Quantity,
                TotalPrice = product.Price * product.Quantity
            }));

            var order = new Order
            {
                UserId = userId,
                Name = name,
                Phone = phone,
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems,
                Status = Status.Waiting,
                AddressId = address.Id
            };

            _context.Orders.Add(order);

            _context.CartItems.RemoveRange(_context.CartItems
                .Where(ci => ci.UserId == userId));

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateStatus(int orderId, Status status)
        {
            var response = new ServiceResponse<bool>();
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }
            order.Status = status;
            await _context.SaveChangesAsync();
            if (status == Status.Delivered)
                await _saleService.CreateOrUpdateSaleAsync(order.TotalPrice);

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> UpdateIsComment(int orderItemId)
        {
            var response = new ServiceResponse<bool>();
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                response.Success = false;
                response.Message = "Order item not found.";
                return response;
            }
            orderItem.IsCommented = true;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
