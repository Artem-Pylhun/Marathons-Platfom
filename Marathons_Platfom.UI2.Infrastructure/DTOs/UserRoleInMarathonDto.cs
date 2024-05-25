namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class UserRoleInMarathonDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int MarathonId { get; set; }
        public int RoleId { get; set; }

    }
    public class UserRoleInMarathonCreateDto
    {
        public int UserId { get; set; }
        public int MarathonId { get; set; }
        public int RoleId { get; set; }
    }

}
