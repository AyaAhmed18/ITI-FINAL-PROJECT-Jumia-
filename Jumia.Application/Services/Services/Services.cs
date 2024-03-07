using Jumia.Application.Services.IServices;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        public Task<ResultView<T>> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDataForPagination<T>> GetAllPagination(int items, int pagenumber)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOne(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<T>> HardDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<T>> SoftDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<T>> UpdateAsync(T book)
        {
            throw new NotImplementedException();
        }
    }
}
