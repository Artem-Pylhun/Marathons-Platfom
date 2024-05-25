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
    public class BadgeMarathonRepository : IRepository<BadgeMarathon>
    {
        private readonly MarathonPlatformContext _context;
        public BadgeMarathonRepository(MarathonPlatformContext context)
        {
            _context = context;
        }
        public async Task Add(BadgeMarathon entity)
        {
            await _context.Set<BadgeMarathon>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(BadgeMarathon entity)
        {
            _context.Set<BadgeMarathon>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<BadgeMarathon>> GetAllAsync()
        {
            return await _context.Set<BadgeMarathon>().ToListAsync();
        }

        public async Task<BadgeMarathon> GetByIdAsync(int id)
        {
            return await _context.Set<BadgeMarathon>().FindAsync(id);
        }

        public IQueryable<BadgeMarathon> GetQueryable()
        {
            return _context.Set<BadgeMarathon>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(BadgeMarathon entity)
        {
            _context.Set<BadgeMarathon>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
