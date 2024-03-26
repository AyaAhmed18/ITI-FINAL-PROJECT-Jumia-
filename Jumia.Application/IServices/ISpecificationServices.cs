using Jumia.Dtos.Product;
using Jumia.Dtos.Specification;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface ISpecificationServices
    {
        Task<ResultDataForPagination<GetAllSpecificationDto>> GetAll();
        Task<ResultView<GetAllSpecificationDto>> GetOne(int ID);
    }
}
