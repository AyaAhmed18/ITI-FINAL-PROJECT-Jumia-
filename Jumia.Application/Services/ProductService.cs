using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Product;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class ProductService:IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _unitOfWork.ProductRepository.GetAllAsync());
            var Prds = AlldAta.Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllProducts()
                                              {
                                                  Id = p.Id,
                                                  Name = p.Name,
                                                  Price = p.RealPrice,
                                                  SubCategoryName = p.SubCategory.Name
                                              }).ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            //resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
    }
}
