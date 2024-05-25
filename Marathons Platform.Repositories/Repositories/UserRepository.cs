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
    public class UserRepository : IRepository<User>
    {
        private readonly MarathonPlatformContext _context;
        public UserRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public IQueryable<User> GetQueryable()
        {
            return _context.Set<User>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await SaveChangesAsync();

        }

        public async Task Delete(User entity)
        {
            _context.Set<User>().Remove(entity);
                       await SaveChangesAsync();

        }

        public async Task Update(User entity)
        {
            _context.Set<User>().Update(entity);
                       await SaveChangesAsync();

        }
        
    }
}
