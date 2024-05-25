using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IMarathonService<T, TCreateDto, TUpdateDto> where T : class where TCreateDto : class where TUpdateDto : class
    {
        List<T> Marathons{ get; set; }
        Task<List<T>> GetMarathons();
        Task<T> GetMarathonById(int id);
        Task AddMarathon(TCreateDto marathon);
        Task DelMarathon(int id);
        Task UpdateMarathon(T marathon);
    }
}
