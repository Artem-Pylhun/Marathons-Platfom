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
    public class ThemeRepository : IRepository<Theme>
    {
        private readonly MarathonPlatformContext _context;
        public ThemeRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Theme>> GetAllAsync()
        {
            return await _context.Set<Theme>().ToListAsync();
        }

        public async Task<Theme> GetByIdAsync(int id)
        {
            return await _context.Set<Theme>().FindAsync(id);
        }

        public IQueryable<Theme> GetQueryable()
        {
            return _context.Set<Theme>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(Theme entity)
        {
            await _context.Set<Theme>().AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(Theme entity)
        {
            _context.Set<Theme>().Remove(entity);
                       await SaveChangesAsync();

        }

        public async Task Update(Theme entity)
        {
            _context.Set<Theme>().Update(entity);
                       await SaveChangesAsync();

        }
    }
}
