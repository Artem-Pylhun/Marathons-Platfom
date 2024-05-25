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
    public class RoleRepository : IRepository<Role>
    {
        private readonly MarathonPlatformContext _context;
        public RoleRepository(MarathonPlatformContext context)
        {
            _context = context;
        }
        public async Task Add(Role entity)
        {
            await _context.Set<Role>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(Role entity)
        {
            _context.Set<Role>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Set<Role>().ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Set<Role>().FindAsync(id);
        }

        public IQueryable<Role> GetQueryable()
        {
            return _context.Set<Role>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(Role entity)
        {
            _context.Set<Role>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
