
namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class MarathonDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ThemeId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Duration { get; set; }
        public int NumOfParticipants { get; set; }
    }
    public class MarathonCreateDto
    {
        public string Title { get; set; }
        public int ThemeId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Duration { get; set; }
    }
    public class MarathonUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ThemeId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfStart { get; set; }
        public int Duration { get; set; }
    }
}
