using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        protected JumiaContext _jumiaContext;
        public OrderRepository(JumiaContext jumiaContext) : base(jumiaContext)
        {
            _jumiaContext = jumiaContext;
        }

        public async Task<List<Order>> FindAll(Expression<Func<Order, bool>>? filter = null, int? pageNumber = null, int? pageSize = null,
           Expression<Func<Order, object>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<Order> query = _jumiaContext.Set<Order>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

    }
}
