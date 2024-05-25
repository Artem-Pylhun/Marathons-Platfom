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
    public class MarathonRepository : IRepository<Marathon>
    {
        private readonly MarathonPlatformContext _context;
        public MarathonRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marathon>> GetAllAsync()
        {
            return await _context.Set<Marathon>().ToListAsync();
        }

        public async Task<Marathon> GetByIdAsync(int id)
        {
            return await _context.Set<Marathon>().FindAsync(id);
        }

        public IQueryable<Marathon> GetQueryable()
        {
            return _context.Set<Marathon>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(Marathon entity)
        {
            await _context.Set<Marathon>().AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(Marathon entity)
        {
            _context.Set<Marathon>().Remove(entity);
                       await SaveChangesAsync();

        }

        public async Task Update(Marathon entity)
        {
            _context.Set<Marathon>().Update(entity);
                       await SaveChangesAsync();

        }
    }
}
