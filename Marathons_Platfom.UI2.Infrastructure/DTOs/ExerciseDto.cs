namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DayNum { get; set; }
        public int MarathonId { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
    public class ExerciseCreateDto
    {
        public int DayNum { get; set; }
        public string Title { get; set; }
        public int MarathonId { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
    public class ExerciseUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MarathonId { get; set; }
        public int DayNum { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
}
