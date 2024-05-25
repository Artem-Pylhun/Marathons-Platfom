using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IExerciseService<T,TCreateDto,TUpdateDto> where T : class where TCreateDto : class where TUpdateDto : class
    {
        List<ExerciseDto> Exercises { get; set; }
        Task<List<ExerciseDto>> GetExercises();
        Task<ExerciseDto> GetExerciseById(int id);
        Task<List<ExerciseDto>> GetExercisesByMarathonId(int id);
        Task AddExercise(ExerciseCreateDto Exercise);
        Task DelExercise(int id);
        Task UpdateExercise(ExerciseUpdateDto Exercise);
    }
}
