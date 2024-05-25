using Marathons_Platfom.Core.Context;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platform.Repositories.Repositories
{
    public class UserRoleInMarathonRepository: IRepository<UserRoleInMarathon>
    {
        private readonly MarathonPlatformContext _context;
        public UserRoleInMarathonRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task Add(UserRoleInMarathon entity)
        {
            await _context.Set<UserRoleInMarathon>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(UserRoleInMarathon entity)
        {
            _context.Set<UserRoleInMarathon>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRoleInMarathon>> GetAllAsync()
        {
            return await _context.Set<UserRoleInMarathon>().ToListAsync();
        }
        public List<UserRoleInMarathon> GetByUserId(int userId)
        {
            return GetQueryable().Where(urin => urin.UserId == userId).ToList();
        }
        public async Task<UserRoleInMarathon> GetByIdAsync(int id)
        {
            return await _context.Set<UserRoleInMarathon>().FindAsync(id);
        }

        public IQueryable<UserRoleInMarathon> GetQueryable()
        {
            return _context.Set<UserRoleInMarathon>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(UserRoleInMarathon entity)
        {
            _context.Set<UserRoleInMarathon>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
