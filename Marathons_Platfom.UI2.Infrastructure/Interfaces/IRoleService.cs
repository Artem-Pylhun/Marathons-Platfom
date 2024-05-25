namespace Marathons_Platfom.UI2.Infrastructure.Interfaces
{
    public interface IRoleService<T, TCreateDto>
    {
        List<T> Roles { get; set; }
        Task<List<T>> GetRoles();
        Task<T> GetRoleById(int id);
        Task AddRole(TCreateDto role);
        Task DelRole(int id);
        Task UpdateRole(T role);
    }
}
