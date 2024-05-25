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
    public class ExerciseRepository: IRepository<Exercise>
    {
        private readonly MarathonPlatformContext _context;
        public ExerciseRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _context.Set<Exercise>().ToListAsync();
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            return await _context.Set<Exercise>().FindAsync(id);
        }

        public IQueryable<Exercise> GetQueryable()
        {
            return _context.Set<Exercise>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(Exercise entity)
        {
            await _context.Set<Exercise>().AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(Exercise entity)
        {
            _context.Set<Exercise>().Remove(entity);
            await SaveChangesAsync();

        }

        public async Task Update(Exercise entity)
        {
            _context.Set<Exercise>().Update(entity);
            await SaveChangesAsync();

        }
        public async Task<List<Exercise>> GetByMarathonIdAsync(int id)
        {
            return await _context.Set<Exercise>().Where(ex => ex.MarathonId == id).ToListAsync();
        }
    }
}
