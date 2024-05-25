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
    public class UserBadgeRepository : IRepository<User_Badge>
    {
        private readonly MarathonPlatformContext _context;
        public UserBadgeRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User_Badge>> GetAllAsync()
        {
            return await _context.Set<User_Badge>().ToListAsync();
        }

        public async Task<User_Badge> GetByIdAsync(int id)
        {
            return await _context.Set<User_Badge>().FindAsync(id);
        }

        public IQueryable<User_Badge> GetQueryable()
        {
            return _context.Set<User_Badge>();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Add(User_Badge entity)
        {
            await _context.Set<User_Badge>().AddAsync(entity);
                       await SaveChangesAsync();

        }

        public async Task Delete(User_Badge entity)
        {
            _context.Set<User_Badge>().Remove(entity);
            await SaveChangesAsync();

        }

        public async Task Update(User_Badge entity)
        {
            _context.Set<User_Badge>().Update(entity);
                       await SaveChangesAsync();

        }
    }
}
