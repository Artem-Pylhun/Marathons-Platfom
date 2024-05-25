namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class UserBadgeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime Claimed { get; set; }

    }

    public class UserBadgeCreateDto
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime Claimed { get; set; }
    }

}
