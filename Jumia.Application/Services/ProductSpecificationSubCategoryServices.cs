using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.ProductSpecificationSubCategory;
using Jumia.Dtos.Specification;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class ProductSpecificationSubCategoryServices : IProductSpecificationSubCategoryServices
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductSpecificationSubCategoryServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //  _stringLocalizer = stringLocalizer;
        }

        public async Task<ResultDataForPagination<GetAllProductSpecificationSubCategory>> GetAll(int item, int pagnumber)
        {
            var AlldAta = (await _unitOfWork.productSpecificationSubCategoryRepository.GetAllAsync());
            var productSpecificationSubCategory = AlldAta.ToList();
            var productSpecificationSubCategories = _mapper.Map<List<GetAllProductSpecificationSubCategory>>(productSpecificationSubCategory);
            ResultDataForPagination<GetAllProductSpecificationSubCategory> resultDataList = new ResultDataForPagination<GetAllProductSpecificationSubCategory>();
            resultDataList.Entities = productSpecificationSubCategories;
            //resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
        public async Task<ResultView<CreateOrUpdateProductSpecificationSubCategory>> Create(CreateOrUpdateProductSpecificationSubCategory productSpecificationSubCategory)
        {
            try
            {
                var Data = await _unitOfWork.productSpecificationSubCategoryRepository.GetAllAsync();
                var OldPrdSpecSubCat = Data.Where(c => c.Id == productSpecificationSubCategory.Id).FirstOrDefault();

                if (OldPrdSpecSubCat != null)
                {
                    return new ResultView<CreateOrUpdateProductSpecificationSubCategory> { Entity = null, IsSuccess = false, Message = "This Specification Already Exist in this SubCategory" };
                }
                else
                {
                    var PrdSpecSubCat = _mapper.Map<ProductSpecificationSubCategory>(productSpecificationSubCategory);
                    var NewPrdSpecSubCat = await _unitOfWork.productSpecificationSubCategoryRepository.CreateAsync(PrdSpecSubCat);
                    await _unitOfWork.SaveChangesAsync();
                    var prdSpecSubCatDto = _mapper.Map<CreateOrUpdateProductSpecificationSubCategory>(NewPrdSpecSubCat);
                    return new ResultView<CreateOrUpdateProductSpecificationSubCategory> { Entity = prdSpecSubCatDto, IsSuccess = true, Message = "Order Created Successfully" };
                }

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateProductSpecificationSubCategory>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        }
    }
}

