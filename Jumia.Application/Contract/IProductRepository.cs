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
    public interface IProductRepository:IRepository<Product,int>
    {
        IQueryable<Product> FindAll(Expression<Func<Product, bool>>? criteria, int? skip, int? take,
          Expression<Func<Product, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
    }
}
