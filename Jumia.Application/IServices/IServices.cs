using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IServices<TEntity,Tid,T> where TEntity : class where T : BaseEntity
    {
        Task<ResultView<TEntity>> CreateAsync(TEntity entity);
        Task<ResultView<TEntity>> HardDelete(TEntity entity);
        Task<ResultView<TEntity>> SoftDelete(TEntity entity);
        Task<ResultDataForPagination<TEntity>> GetAllPagination(int items, int pagenumber);
        Task<TEntity> GetOne(Tid ID);
        Task<ResultView<TEntity>> UpdateAsync(TEntity book);
    }
}
