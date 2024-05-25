namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class ProgressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MarathonId { get; set; }
        public int Percent { get; set; }
    }

    public class ProgressCreateDto
    {
        public int UserId { get; set; }
        public int MarathonId { get; set; }
        public int Percent { get; set; }
    }
    public class ProgressAddDto
    {
        public int UserId { get; set; }
        public int MarathonId { get; set; }
    }
}
