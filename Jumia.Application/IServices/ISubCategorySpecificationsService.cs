using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.DTOS.ViewResultDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface ISubCategorySpecificationsService
    {
        Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Create(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto);

        //Update
        Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Update(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto);

        // Delete
        Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Delete(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto);

        // GetAll
      //  Task<ResultDataForPagination<GetAllCategoryDto>> GetAll(int item, int pagnumber);

        //GetOne
      //  Task<ResultView<GetAllCategoryDto>> GetOne(int id);

    }
}
