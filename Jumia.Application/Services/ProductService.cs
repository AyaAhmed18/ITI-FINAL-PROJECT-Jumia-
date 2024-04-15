using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Product;
using Jumia.Dtos.ProductSpecificationSubCategory;
using Jumia.Dtos.Specification;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Jumia.Model.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //private readonly IProductRepository _productRepository;
        //private readonly IMapper _mapper;

        //public ProductService(IProductRepository productRepository, IMapper mapper)
        //{
        //    _productRepository = productRepository;
        //    _mapper = mapper;
        //}
        public async Task<ResultDataForPagination<GetAllProducts>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _unitOfWork.ProductRepository.GetAllAsync())
                .Include(Prd => Prd.SubCategory)
                .Include(Prd => Prd.Brand);

            var Prds = AlldAta
                .Skip(items * (pagenumber - 1))
                .Take(items)
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = AlldAta.Count();
            return resultDataList;
        }


        //Create
        public async Task<ResultView<CreateOrUpdateProductDto>> Create(CreateOrUpdateProductDto ProductDto, List<IFormFile> images)
        {
            var Data = await _unitOfWork.ProductRepository.GetAllAsync();
            var OldProduct = Data.Where(c => c.Name == ProductDto.Name).FirstOrDefault();

            if (OldProduct != null)
            {
                return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = "Product Already Exist" };

            }
            else
            {

                if (images != null)
                {
                    foreach (var image in images)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);

                            ProductDto.Images.Add(memoryStream.ToArray());
                        }
                    }
                }


                var ProductEnt = _mapper.Map<Product>(ProductDto);
                var NewProduct = await _unitOfWork.ProductRepository.CreateAsync(ProductEnt);
                await _unitOfWork.ProductRepository.SaveChangesAsync();
                var productDto = _mapper.Map<CreateOrUpdateProductDto>(NewProduct);


                return new ResultView<CreateOrUpdateProductDto> { Entity = productDto, IsSuccess = true, Message = "Product Created Successfully" };
            }


        }

        public async Task<ResultView<CreateOrUpdateProductDto>> Update(CreateOrUpdateProductDto productDto, List<IFormFile> images)
        {

            var Oldproduct = await _unitOfWork.ProductRepository.GetOneAsync(productDto.Id);
            if (Oldproduct == null)
            {
                return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = "product Not Found!" };

            }
            else
            {
                //_mapper.Map<product>(productDto);
                _mapper.Map(productDto, Oldproduct);
                if (images != null)
                {
                    foreach (var image in images)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await image.CopyToAsync(memoryStream);

                            productDto.Images.Add(memoryStream.ToArray());
                        }
                    }
                }

                var UPproduct = await _unitOfWork.ProductRepository.UpdateAsync(Oldproduct);
                await _unitOfWork.ProductRepository.SaveChangesAsync();
                var ProductDto = _mapper.Map<CreateOrUpdateProductDto>(UPproduct);

                return new ResultView<CreateOrUpdateProductDto> { Entity = ProductDto, IsSuccess = true, Message = "product Updated Successfully" };
            }
        }







        // delete
        public async Task<ResultView<CreateOrUpdateProductDto>> Delete(CreateOrUpdateProductDto productDto)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetOneAsync(productDto.Id);
                if (product == null)
                {
                    return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = "product Not Found!" };
                }

                await _unitOfWork.ProductRepository.DeleteAsync(product);
                await _unitOfWork.ProductRepository.SaveChangesAsync();

                var ProductDto = _mapper.Map<CreateOrUpdateProductDto>(product);
                return new ResultView<CreateOrUpdateProductDto> { Entity = ProductDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }







        //GetOne
        public async Task<ResultView<CreateOrUpdateProductDto>> GetOne(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetOneAsync(id);
            if (product == null)
            {
                return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = "Not Found!" };
            }
            else
            {
                var productDto = _mapper.Map<CreateOrUpdateProductDto>(product);

                return new ResultView<CreateOrUpdateProductDto> { Entity = productDto, IsSuccess = true, Message = "Succses" };
            }
        }
        //public async Task<ResultView<GetAllSpecificationDto>> GetSpecificationsOfProduct(int SubCategoryId)
        //{
        //    try
        //    {
        //        var Specs = await (_unitOfWork.SubCategoryRepository.GetAllAsync());
        //        var specs = Specs.Where(x => x.Id == SubCategoryId).Contains(x =)//.Select(x => new GetAllSpecificationDto()
        //        {
        //            Id = x.Specifications.First().Id,
        //            Name =
        //        })
        //        var Specs = await (_unitOfWork.SubCategorySpecificationRepository.GetAllAsync());


        //        var Specs = await (_unitOfWork.SubCategorySpecificationRepository.GetAllAsync());
        //        var SpecsUnderCondition = Specs.Where(x => x.SubCategoryId == SubCategoryId);
        //        var FinalSpecs = await (_unitOfWork.SpecificationRepository.GetAllAsync());
        //        var ff = FinalSpecs.Where(x => x.)

        //    }
        //    catch
        //    {

        //    }
        //}

        //Create With Specification And SubCategory
        public async Task<ResultView<CreateOrUpdateProductSpecificationSubCategory>> CreateWithSpecificationValue(CreateOrUpdateProductSpecificationSubCategory productSpecificationSubCategory)
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




        //public async Task<ResultView<CreateOrUpdateProductDto>> HardDelete(CreateOrUpdateProductDto product)
        //{
        //    try
        //    {
        //        var PRd = _mapper.Map<Product>(product);
        //        var Oldprd = _productRepository.DeleteAsync(PRd);
        //        await _productRepository.SaveChangesAsync();
        //        var PrdDto = _mapper.Map<CreateOrUpdateProductDto>(Oldprd);
        //        return new ResultView<CreateOrUpdateProductDto> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = ex.Message };

        //    }
        //}
        //public async Task<ResultView<CreateOrUpdateProductDto>> SoftDelete(CreateOrUpdateProductDto product)
        //{
        //    try
        //    {
        //        var PRd = _mapper.Map<Product>(product);
        //        var Oldprd = (await _productRepository.GetAllAsync()).FirstOrDefault(p => p.Id == product.Id);
        //        Oldprd.IsDeleted = true;
        //        await _productRepository.SaveChangesAsync();
        //        var PrdDto = _mapper.Map<CreateOrUpdateProductDto>(Oldprd);
        //        return new ResultView<CreateOrUpdateProductDto> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultView<CreateOrUpdateProductDto> { Entity = null, IsSuccess = false, Message = ex.Message };

        //    }
        //}


        //public async Task<CreateOrUpdateProductDto> GetOne(int ID)
        //{
        //    var prd = await _productRepository.GetByIdAsync(ID);
        //    var REturnPrd = _mapper.Map<CreateOrUpdateProductDto>(prd);
        //    return REturnPrd;
        //}
        public async Task<ResultView<GetAllProducts>> Getbyname(string name)
        {
            //   var product = await _unitOfWork..Getbyname(name);
            var product = (await _unitOfWork.ProductRepository.GetAllAsync()).Where(p => p.Name == name).FirstOrDefault();
            if (product == null)
            {
                return new ResultView<GetAllProducts> { Entity = null, IsSuccess = false, Message = "Not Found!" };
            }
            else
            {
                var productDto = _mapper.Map<GetAllProducts>(product);

                return new ResultView<GetAllProducts> { Entity = productDto, IsSuccess = true, Message = "Succses" };
            }
        }

        public async Task<ResultDataForPagination<GetAllProducts>> GetOrderedAsc()
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.RealPrice, OrderBy.Ascending))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            return resultDataList;
        }

        public async Task<ResultDataForPagination<GetAllProducts>> GetOrderedDsc()
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.RealPrice, OrderBy.Descending))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetNewestArrivals()
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.CreatedDate, OrderBy.Descending))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            return resultDataList;
        }




        public async Task<ResultDataForPagination<GetAllProducts>> Search(string PartialName)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => Prd.Name.Contains(PartialName) || Prd.ShortDescription.Contains(PartialName), null, null))
               .Select(p => new GetAllProducts(p))
               .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            return resultDataList;
        }
        //FilterByPriceRange
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByPriceRange(int MinPrice, int MaxPrice)
        {

            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => Prd.RealPrice >= MinPrice && Prd.RealPrice <= MaxPrice, null, null))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByDiscountRange(int MinDisc)
        {

            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => Prd.Discount >= MinDisc, null, null))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByDiscountRangeToSlider(int MinDisc, int items, int pagenumber)
        {

            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => Prd.Discount >= MinDisc, null, null))
                .Skip(items * (pagenumber - 1))
                .Take(items)
                .Select(p => new GetAllProducts(p))
                
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByBrandName(int BrandId)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => Prd.BrandId == BrandId, null, null))
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByAll(List<int>? BrandIdList, int? MinPrice, int? MaxPrice, int? MinDisc)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd =>
            (BrandIdList == null || BrandIdList.Any(brandId => brandId == Prd.BrandId))
            && (MinDisc == null || Prd.Discount >= MinDisc)
            && (MinPrice == null || Prd.RealPrice >= MinPrice)
            && (MaxPrice == null || Prd.RealPrice <= MaxPrice)
            , null, null))
              .Include(Prd => Prd.SubCategory)
              .Include(Prd => Prd.Brand)
              .Select(p => new GetAllProducts(p))
              .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByAll(List<int>? BrandIdList, int? MinPrice, int? MaxPrice, int? MinDisc, int items, int pagenumber)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd =>
            (BrandIdList == null || BrandIdList.Any(brandId => brandId == Prd.BrandId))
            && (MinDisc == null || Prd.Discount >= MinDisc)
            && (MinPrice == null || Prd.RealPrice >= MinPrice)
            && (MaxPrice == null || Prd.RealPrice <= MaxPrice)
            , null, null))
            .Include(Prd => Prd.SubCategory)
            .Include(Prd => Prd.Brand);
            var PrdsAfterFiltering = Prds
              .Skip(items * (pagenumber - 1))
              .Take(items)
              .Select(p => new GetAllProducts(p))
              .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = PrdsAfterFiltering;
            resultDataList.count = Prds.Count();
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> FilterByBrandList(List<int> BrandIdList)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(Prd => BrandIdList.Any(brandId => brandId == Prd.BrandId), null, null))
              .Select(p => new GetAllProducts(p))
              .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = Prds;
            resultDataList.count = Prds.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> MakeFinalResult(List<GetAllProducts> FinalResult)
        {
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = FinalResult;
            resultDataList.count = FinalResult.Count;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetProductsByCategoryId(int CategoryId, int items, int pagenumber)
        {

            var productsInCategory = _unitOfWork.ProductRepository.FindAll(
                criteria: product => product.SubCategory.CategoryId == CategoryId
                , null
                , null);

            var productsInCategoryAfterFeltering = productsInCategory
             .Skip(items * (pagenumber - 1))
             .Take(items)
             .Select(p => new GetAllProducts(p))
             .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = productsInCategoryAfterFeltering;
            resultDataList.count = productsInCategory.Count();
            return resultDataList;

        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetProductsBySubCategoryId(int SubCategoryId, int items, int pagenumber)
        {


            var productsInSubCategory = _unitOfWork.ProductRepository.FindAll(
                criteria: product => product.SubCategoryId == SubCategoryId
                , null
                , null);
            var productsInSubCategoryAfterFeltering = productsInSubCategory
                .Skip(items * (pagenumber - 1))
                .Take(items)
                .Select(p => new GetAllProducts(p))
                .ToList();
            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = productsInSubCategoryAfterFeltering;
            resultDataList.count = productsInSubCategory.Count();
            return resultDataList;
            //var ProductsInCategory = MakeFinalResult(productsInCategoryAfterFeltering);

        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetOrderedAscWithPagination(int items, int pagenumber)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.RealPrice, OrderBy.Ascending));
                //.Select(p => new GetAllProducts(p))
                //.ToList();
            var productsSortedAscInPagination = Prds
              .Skip(items * (pagenumber - 1))
              .Take(items)
              .Select(p => new GetAllProducts(p))
              .ToList();

            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = productsSortedAscInPagination;
            return resultDataList;
        }

        public async Task<ResultDataForPagination<GetAllProducts>> GetOrderedDscWithPagination(int items, int pagenumber)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.RealPrice, OrderBy.Descending));
                //.Select(p => new GetAllProducts(p))
                //.ToList();
            var productsSortedDscInPagination = Prds
              .Skip(items * (pagenumber - 1))
              .Take(items)
              .Select(p => new GetAllProducts(p))
              .ToList();

            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = productsSortedDscInPagination;
            return resultDataList;
        }
        public async Task<ResultDataForPagination<GetAllProducts>> GetNewestArrivalsWithPagination(int items, int pagenumber)
        {
            var Prds = (_unitOfWork.ProductRepository.FindAll(null, null, null, Prd => Prd.CreatedDate, OrderBy.Descending));
                //.Select(p => new GetAllProducts(p))
                //.ToList();

            var productsSortedDscInPagination = Prds
               .Skip(items * (pagenumber - 1))
               .Take(items)
               .Select(p => new GetAllProducts(p))
               .ToList();

            ResultDataForPagination<GetAllProducts> resultDataList = new ResultDataForPagination<GetAllProducts>();
            resultDataList.Entities = productsSortedDscInPagination;
            return resultDataList;
        }

        //public async List<GetAllProducts> MakeIncludable(IQueryable<GetAllProducts> Prds, int? items, int? pagenumber)
        //{
        //    Prds.Take(items);
        //}
    }

}