using Jumia.Model;
using Jumia.Model.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Contract
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        Task<List<Order>> FindAll(Expression<Func<Order, bool>>? filter = null, int? pageNumber = null, int? pageSize = null,
         Expression<Func<Order, object>>? orderBy = null, string includeProperties = "");
    }
}
