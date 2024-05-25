using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IUserService<T, TCreateDto, TUpdateDto> where T : class where TCreateDto : class where TUpdateDto : class
    {
        List<UserDto> Users { get; set; }
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int id);
        Task RegisterUser(UserCreateDto user);
        Task LoginUser(UserLogDto request);
        Task DelUser(int id);
        Task UpdateUser(UserDto user);
    }
}
