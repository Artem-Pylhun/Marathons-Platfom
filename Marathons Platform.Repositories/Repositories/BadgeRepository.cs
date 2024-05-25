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
    public class BadgeRepository : IRepository<Badge>
    {
        private readonly MarathonPlatformContext _context;
        public BadgeRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Badge>> GetAllAsync()
        {
            return await _context.Set<Badge>().ToListAsync();
        }

        public async Task<Badge> GetByIdAsync(int id)
        {
            return await _context.Set<Badge>().FindAsync(id);
        }

        public IQueryable<Badge> GetQueryable()
        {
            return _context.Set<Badge>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(Badge entity)
        {
           await _context.Set<Badge>().AddAsync(entity);
           await SaveChangesAsync();
        }

        public async Task Delete(Badge entity)
        {
            _context.Set<Badge>().Remove(entity);
            await SaveChangesAsync();

        }

        public async Task Update(Badge entity)
        {
            _context.Set<Badge>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
