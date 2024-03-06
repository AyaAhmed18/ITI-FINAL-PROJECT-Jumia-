using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Contract
{
    public interface IRepository<TEntity,Tid>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetOneAsync(Tid id);
        Task<int> SaveChangesAsync();
    }
}
