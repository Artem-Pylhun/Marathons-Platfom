namespace Marathons_Platfom.UI2.Infrastructure.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class RoleCreateDto
    {
        public string Title { get; set; }
    }
}
