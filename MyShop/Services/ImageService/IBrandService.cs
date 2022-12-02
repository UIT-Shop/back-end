namespace MyShop.Services.ImageService
{
    public interface IImageService
    {
        // Create
        Task<ServiceResponse<Image>> CreateImage(Image image);

        // Read
        Task<ServiceResponse<List<Image>>> GetImages();
        Task<ServiceResponse<Image>> GetImage(int id);

        // Update
        Task<ServiceResponse<Image>> UpdateImage(Image image);

        // Delete
        Task<ServiceResponse<bool>> DeleteImage(int imageId);
    }
}
