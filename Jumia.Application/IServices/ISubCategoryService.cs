using Jumia.Application.Contract;
using Jumia.Dtos.SubCategory;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.IServices
{
    public interface ISubCategoryService
    {

        Task<ResultView<CreateOrUpdateSubDto>> Create(CreateOrUpdateSubDto subcategoryDto);


        Task<ResultView<CreateOrUpdateSubDto>> Update(CreateOrUpdateSubDto subcategoryDto);

        Task<ResultView<CreateOrUpdateSubDto>> Delete(CreateOrUpdateSubDto subcategoryDto);

        Task<ResultDataForPagination<GetAllSubDto>> GetAll(int item, int pagnumber);

        Task<ResultView<GetAllSubDto>> GetOne(int id);










    }
}
