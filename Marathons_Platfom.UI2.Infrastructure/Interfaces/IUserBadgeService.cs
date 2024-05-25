using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IUserBadgeService<T, TCreateDto> where T:class where TCreateDto:class
    {
        List<T> UserBadges { get; set; }
        Task<List<T>> GetUserBadges();
        Task<T> GetUserBadgeById(int id);
        Task AddUserBadge(TCreateDto uBadge);
        Task DelUserBadge(int id);
        Task UpdateUserBadge(T uBadge);
    }
}
