using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platform.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync (int id);
        IQueryable<T> GetQueryable();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

        Task SaveChangesAsync();
    }
}
