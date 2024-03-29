using Jumia.Application.Contract;
using Jumia.Context;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jumia.Application.Contract.IBrandRepository;

namespace Jumia.Infrastructure
{
    public class BrandRepository : Repository<Brand, int>, IBrandRepository
    {
        public BrandRepository(JumiaContext eCommerceContext) : base(eCommerceContext)
        {
        }
    }
}
