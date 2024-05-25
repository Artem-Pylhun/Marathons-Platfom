using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IThemeService<T, TCreateDto> where T : class where TCreateDto : class
    {
        List<T> Themes { get; set; }
        Task<List<ThemeDto>> GetThemes();
        Task<T> GetThemeById(int id);
        Task AddTheme(TCreateDto theme);
        Task DelTheme(int id);
        Task UpdateTheme(T theme);
    }
}
