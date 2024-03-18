using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class Repository<TEntity, Tid> : IRepository<TEntity, Tid> where TEntity : class
    {
        private readonly JumiaContext _eCommerceContext;
        public readonly DbSet<TEntity> _DbsetEntity;
        public Repository(JumiaContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
            _DbsetEntity = eCommerceContext.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _DbsetEntity.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_DbsetEntity.Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_DbsetEntity.Select(s => s));
        }

        public async Task<TEntity> GetOneAsync(Tid id)
        {
            return await _DbsetEntity.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _eCommerceContext.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            _eCommerceContext.ChangeTracker.Clear();
            return Task.FromResult(_DbsetEntity.Update(entity).Entity);
        }
    }

}
