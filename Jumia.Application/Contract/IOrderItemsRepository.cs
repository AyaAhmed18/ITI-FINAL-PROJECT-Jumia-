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
    public interface IOrderItemsRepository : IRepository<OrderItems, int>
    {
        Task<List<OrderItems>> FindAll(Expression<Func<OrderItems, bool>>? filter = null, int? pageNumber = null, int? pageSize = null,
        Expression<Func<OrderItems, object>>? orderBy = null, string includeProperties = "");
    }
}
