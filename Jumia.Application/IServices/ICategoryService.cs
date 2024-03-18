using Jumia.Application.Contract;
using Jumia.Dtos.Category;
using Jumia.DTOS.ViewResultDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface ICategoryService
    {


        //Create
        Task<ResultView<CreateOrUpdateCategoryDto>> Create(CreateOrUpdateCategoryDto categoryDto, IFormFile image);

        //Update
        Task<ResultView<CreateOrUpdateCategoryDto>> Update(CreateOrUpdateCategoryDto categoryDto, IFormFile image);

        // Delete
        Task<ResultView<CreateOrUpdateCategoryDto>> Delete(CreateOrUpdateCategoryDto categoryDto);

        // GetAll
        Task<ResultDataForPagination<GetAllCategoryDto>> GetAll(int item, int pagnumber);

        //GetOne
        Task<ResultView<GetAllCategoryDto>> GetOne(int id);











    }
}
