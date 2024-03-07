using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.IServices
{
    public interface IServices<T> where T : class
    {
        Task<ResultView<T>> Create(T entity);
        Task<ResultView<T>> HardDelete(T entity);
        Task<ResultView<T>> SoftDelete(T entity);
        Task<ResultDataForPagination<T>> GetAllPagination(int items, int pagenumber);
        Task<T> GetOne(int ID);
        Task<ResultView<T>> UpdateAsync(T book);
    }
}
