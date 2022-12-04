namespace MyShop.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ImageService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Image>> CreateImage(Image image)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin)))
                return new ServiceResponse<Image> { Success = false, Message = "You are not allow to do this action" };
            if (image.ColorId != null)
            {
                var dbImage = await _context.Images
                .FirstOrDefaultAsync(p => p.ProductId == image.ProductId && p.ColorId == image.ColorId);
                if (dbImage != null)
                    return new ServiceResponse<Image> { Data = null, Message = "Only have one picture for each color of product" };
            }

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Image> { Data = image };
        }

        public async Task<ServiceResponse<bool>> DeleteImage(int imageId)
        {
            var dbImage = await _context.Images.FindAsync(imageId);
            if (dbImage == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Image not found."
                };
            }
            _context.Images.Remove(dbImage);
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<Image>> GetImage(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(p => p.Id == id);
            return image == null
                ? new ServiceResponse<Image>
                {
                    Success = false,
                    Message = "Image not found"
                }
                : new ServiceResponse<Image>
                {
                    Data = image
                };
        }

        public async Task<ServiceResponse<List<Image>>> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return new ServiceResponse<List<Image>>
            {
                Data = images
            };
        }

        public async Task<ServiceResponse<Image>> UpdateImage(Image image)
        {
            var dbImage = await _context.Images
                .FirstOrDefaultAsync(p => p.Id == image.Id);

            if (dbImage == null)
            {
                return new ServiceResponse<Image>
                {
                    Success = false,
                    Message = "Image not found."
                };
            }

            dbImage.ProductId = image.ProductId;
            dbImage.Url = image.Url;
            dbImage.ColorId = image.ColorId;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Image> { Data = image };
        }
    }
}
