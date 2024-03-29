using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Model;
using Jumia.Model.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class ProductRepository : Repository<Product, int>, IProductRepository 
    {
        protected JumiaContext _jumiaContext;
        public ProductRepository(JumiaContext jumiaContext) : base(jumiaContext)
        {
            _jumiaContext = jumiaContext;
        }
        public IQueryable<Product> FindAll(Expression<Func<Product, bool>> criteria, int? skip, int? take,
          Expression<Func<Product, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<Product> query = _jumiaContext.Set<Product>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query;
        }

    }
}
