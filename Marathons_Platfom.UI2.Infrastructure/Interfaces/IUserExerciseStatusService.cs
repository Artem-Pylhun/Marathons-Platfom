using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IUserExerciseStatusService<T, TCreateDto> where T : class where TCreateDto : class
    {
        List<T> ExerciseStatuses { get; set; }
        Task<List<T>> GetExerciseStatuses();
        Task<T> GetExerciseStatusById(int id);
        Task AddExerciseStatus(TCreateDto exStatus);
        Task DelExerciseStatus(int id);
        Task UpdateExerciseStatus(T exStatus);
        Task<T> GetStatusByUserAndExerciseId(int userId, int marathonId);

    }
}
