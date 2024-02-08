using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
