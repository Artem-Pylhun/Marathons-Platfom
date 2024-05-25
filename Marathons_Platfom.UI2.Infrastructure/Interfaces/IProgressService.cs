namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IProgressService<T, TCreateDto, TUpdateDto> where T: class where TCreateDto: class where TUpdateDto: class
    {
        List<T> Progresses { get; set; }
        Task<List<T>> GetProgresses();
        Task<T> GetProgressById(int id);
        Task<T> GetProgressByUserAndMarathonId(int userId, int marathonId);
        Task<bool> ExistsAsync(int userId, int marathonId);
        Task AddProgress(TCreateDto progress);
        Task DelProgress(int id);
        Task UpdateProgress(T progress);
    }
}
