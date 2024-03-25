using Jumia.Dtos.ProductSpecificationSubCategory;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IProductSpecificationSubCategoryServices
    {
        Task<ResultView<GetAllProductSpecificationSubCategory>> Create(GetAllProductSpecificationSubCategory productSubCategorySpecificationDto);

        //Update
        Task<ResultView<CreateOrUpdateProductSpecificationSubCategory>> Update(CreateOrUpdateProductSpecificationSubCategory subCategorySpecificationDto);

        // Delete
        Task<ResultView<CreateOrUpdateProductSpecificationSubCategory>> Delete(CreateOrUpdateProductSpecificationSubCategory subCategorySpecificationDto);

        // GetAll
        //  Task<ResultDataForPagination<GetAllCategoryDto>> GetAll(int item, int pagnumber);

        //GetOne
        //  Task<ResultView<GetAllCategoryDto>> GetOne(int id);
    }
}
