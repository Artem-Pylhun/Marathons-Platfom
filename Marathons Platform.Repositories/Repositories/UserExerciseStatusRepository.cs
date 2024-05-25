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
    public class UserExerciseStatusRepository: IRepository<UserExerciseStatus>
    {
        private readonly MarathonPlatformContext _context;
        public UserExerciseStatusRepository(MarathonPlatformContext context)
        {
            _context = context;
        }

        public async Task Add(UserExerciseStatus entity)
        {
            await _context.Set<UserExerciseStatus>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(UserExerciseStatus entity)
        {
            _context.Set<UserExerciseStatus>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<UserExerciseStatus>> GetAllAsync()
        {
            return await _context.Set<UserExerciseStatus>().ToListAsync();
        }

        public async Task<UserExerciseStatus> GetByIdAsync(int id)
        {
            return await _context.Set<UserExerciseStatus>().FindAsync(id);
        }

        public IQueryable<UserExerciseStatus> GetQueryable()
        {
            return _context.Set<UserExerciseStatus>();
        }

        public  Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task Update(UserExerciseStatus entity)
        {
            _context.Set<UserExerciseStatus>().Update(entity);
            await SaveChangesAsync();
        }
        public async Task<UserExerciseStatus> GetByUserAndExerciseAsync(int userId, int exerciseId)
        {
            var ues = await _context.Set<UserExerciseStatus>()
                .FirstOrDefaultAsync(p => p.UserId == userId && p.ExerciseId == exerciseId);

            return ues;
        }
    }
}
