using Jumia.Dtos.Product;
using Jumia.DTOS.ViewResultDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IProductServices
    {
        Task<ResultView<CreateOrUpdateProductDto>> Create(CreateOrUpdateProductDto product, List<IFormFile> images);

        //    Task<ResultView<CreateOrUpdateProductDTO>> HardDelete(CreateOrUpdateProductDTO product);
        //    Task<ResultView<CreateOrUpdateProductDTO>> SoftDelete(CreateOrUpdateProductDTO product);
        Task<ResultDataForPagination<GetAllProducts>> GetAllPagination(int items, int pagenumber);
        Task<ResultView<GetAllProducts>> GetOne(int ID);
        Task<ResultView<CreateOrUpdateProductDto>> Update(CreateOrUpdateProductDto productDto, List<IFormFile> images);
        Task<ResultView<CreateOrUpdateProductDto>> Delete(CreateOrUpdateProductDto productDto);
        Task<ResultView<GetAllProducts>> Getbyname(string name);
        Task<ResultDataForPagination<GetAllProducts>> GetOrderedAsc();
        Task<ResultDataForPagination<GetAllProducts>> GetOrderedDsc();
        Task<ResultDataForPagination<GetAllProducts>> GetNewestArrivals();
        Task<ResultDataForPagination<GetAllProducts>> Search(string PartialName);
        Task<ResultDataForPagination<GetAllProducts>> countProducts();
    }
}
