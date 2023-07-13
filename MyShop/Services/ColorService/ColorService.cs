namespace MyShop.Services.ColorService
{
    public class ColorService : IColorService
    {
        private readonly DataContext _context;

        public ColorService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Color>>> AddColor(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return await GetColors();
        }

        public async Task<ServiceResponse<bool>> DeleteColor(string colorId)
        {
            var dbColor = await _context.Colors.FindAsync(colorId);
            if (dbColor == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Color not found."
                };
            }
            dbColor.Deleted = true;
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Color>>> GetColors()
        {
            var colors = await _context.Colors.Where(b => b.Deleted == false).ToListAsync();
            return new ServiceResponse<List<Color>> { Data = colors };
        }

        public async Task<ServiceResponse<Color>> GetColor(string colorId)
        {
            var color = await _context.Colors.FindAsync(colorId);
            return new ServiceResponse<Color> { Data = color };
        }

        public async Task<ServiceResponse<List<Color>>> UpdateColor(Color color)
        {
            var dbColor = await _context.Colors.FindAsync(color.Id);
            if (dbColor == null)
            {
                return new ServiceResponse<List<Color>>
                {
                    Success = false,
                    Message = "Product Type not found."
                };
            }

            dbColor.Name = color.Name;
            await _context.SaveChangesAsync();

            return await GetColors();
        }
    }
}
