using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IBadgeMarathonService<T, TCreateDto> where T : class where TCreateDto : class
    {
        List<T> BadgeMarathon { get; set; }
        Task<List<T>> GetBadgesMarathon();
        Task<T> GetBadgeMarathonById(int id);
        Task AddBadgeMarathon(TCreateDto bMarathon);
        Task DelBadgeMarathon(int id);
        Task UpdateBadgeMarathon(T bMarathon);
    }
}
