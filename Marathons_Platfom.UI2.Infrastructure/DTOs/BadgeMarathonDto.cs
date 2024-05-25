namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class BadgeMarathonDto
    {
        public int Id { get; set; }
        public int BadgeId { get; set; }
        public int MarathonId { get; set; }

    }
    public class BadgeMarathonCreateDto
    {
        public int BadgeId { get; set; }
        public int MarathonId { get; set; }
    }
}
