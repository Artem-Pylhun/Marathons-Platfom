using Marathons_Platfom.Core.Context;
using Marathons_Platfom.Core.Entities;
using Marathons_Platform.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platform.Domain.Repositories
{
    public class ProgressRepository : IRepository<Progress>
    {
        private readonly MarathonPlatformContext _context;
        public ProgressRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Progress>> GetAllAsync()
        {
            return await _context.Set<Progress>().ToListAsync();
        }

        public async Task<Progress> GetByIdAsync(int id)
        {
            return await _context.Set<Progress>().FindAsync(id);
        }

        public IQueryable<Progress> GetQueryable()
        {
            return _context.Set<Progress>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(Progress entity)
        {
            await _context.Set<Progress>().AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(Progress entity)
        {
            _context.Set<Progress>().Remove(entity);
                       await SaveChangesAsync();

        }

        public async Task Update(Progress entity)
        {
            _context.Set<Progress>().Update(entity);
                       await SaveChangesAsync();

        }
        public async Task<bool> ExistsAsync(int userId, int marathonId)
        {
            return await _context.Set<Progress>()
                .AnyAsync(p => p.UserId == userId && p.MarathonId == marathonId);
        }
        public async Task<Progress> GetByUserAndMarathonAsync(int userId, int marathonId)
        {
            var progress = await _context.Set<Progress>()
                .FirstOrDefaultAsync(p => p.UserId == userId && p.MarathonId == marathonId);

            return progress;
        }
    }
}
