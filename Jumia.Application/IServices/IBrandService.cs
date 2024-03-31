using Jumia.Dtos.Brand;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IBrandService
    {
        Task<ResultDataForPagination<GetAllBrandDto>> GetAll();
    }
}
