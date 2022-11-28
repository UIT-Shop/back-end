namespace MyShop.Services.ColorService
{
    public interface IColorService
    {
        Task<ServiceResponse<List<Color>>> GetColors();
        Task<ServiceResponse<Color>> GetColor(string id);
        Task<ServiceResponse<List<Color>>> AddColor(Color color);
        Task<ServiceResponse<List<Color>>> UpdateColor(Color color);
        Task<ServiceResponse<bool>> DeleteColor(string colorId);
    }
}
