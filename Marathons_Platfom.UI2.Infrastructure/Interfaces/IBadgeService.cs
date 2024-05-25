using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IBadgeService<T, TCreateDto, TUpdateDto> where T : class where TCreateDto : class where TUpdateDto : class
    {
        List<BadgeDto> Badges { get; set; }
        Task<List<BadgeDto>> GetBadges();
        Task<BadgeDto> GetBadgeById(int id);
        Task AddBadge(BadgeCreateDto badge);
        Task DelBadge(int id);
        Task UpdateBadge(BadgeUpdateDto badge);
    }
}
