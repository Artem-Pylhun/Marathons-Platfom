namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class UserExerciseStatusesDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public bool Status { get; set; }
    }
    public class UserExerciseStatusesCreateDto
    {
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
    }
}
