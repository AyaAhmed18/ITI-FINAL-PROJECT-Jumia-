using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Infrastructure
{
    public class ProductSpecificationSubCategoryRepository : Repository<ProductSpecificationSubCategory, int>, IProductSpecificationSubCategoryRepository
    {
        public ProductSpecificationSubCategoryRepository(JumiaContext eCommerceContext) : base(eCommerceContext)
        {

        }
    }
}
