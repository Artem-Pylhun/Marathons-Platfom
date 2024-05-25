using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IUserRoleInMarathon<T, TCreateDto> where T : class where TCreateDto : class
    {
        List<T> UserRolesInMarathon { get; set; }
        Task<List<T>> GetUserRolesInMarathon();
        Task<T> GetUserRolesInMarathonById(int id);
        Task AddUserRolesInMarathon(TCreateDto urin);
        Task DelUserRolesInMarathon(int id);
        Task UpdateUserRolesInMarathon(T urin);
    }
}
