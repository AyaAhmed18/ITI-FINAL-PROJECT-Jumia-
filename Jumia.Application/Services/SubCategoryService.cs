using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategory;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Jumia.Application.Services.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IMapper _mapper;


        public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _mapper = mapper;
        }

        //Create 
        public async Task<ResultView<CreateOrUpdateSubDto>> Create(CreateOrUpdateSubDto subcategoryDto, IFormFile image)
        {
            var Data = await _subCategoryRepository.GetAllAsync();
            var OldSubCategory = Data.Where(c => c.Name == subcategoryDto.Name).FirstOrDefault();

            if (OldSubCategory != null)
            {
                return new ResultView<CreateOrUpdateSubDto> { Entity = null, IsSuccess = false, Message = "SubCategory Already Exist" };

            }
            else
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        subcategoryDto.Image = memoryStream.ToArray();
                    }
                }


                var SubCategory = _mapper.Map<SubCategory>(subcategoryDto);
                var NewSubCategory = await _subCategoryRepository.CreateAsync(SubCategory);
                await _subCategoryRepository.SaveChangesAsync();
                var SubcategoryDto = _mapper.Map<CreateOrUpdateSubDto>(NewSubCategory);


                return new ResultView<CreateOrUpdateSubDto> { Entity = SubcategoryDto, IsSuccess = true, Message = "SubCategory Created Successfully" };
            }


        }


        // Update 
        public async Task<ResultView<CreateOrUpdateSubDto>> Update(CreateOrUpdateSubDto subcategoryDto, IFormFile image)
        {
            
            var OldSubCategory = await _subCategoryRepository.GetOneAsync(subcategoryDto.Id);
            if (OldSubCategory == null)
            {
                return new ResultView<CreateOrUpdateSubDto> { Entity = null, IsSuccess = false, Message = "SubCategory Not Found!" };

            }
            else
            {
                 _mapper.Map(subcategoryDto, OldSubCategory);

                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        OldSubCategory.Image = memoryStream.ToArray();
                    }
                }

                var UPSubCategory = await _subCategoryRepository.UpdateAsync(OldSubCategory);
                await _subCategoryRepository.SaveChangesAsync();
                var SubCategoryDto = _mapper.Map<CreateOrUpdateSubDto>(UPSubCategory);

                return new ResultView<CreateOrUpdateSubDto> { Entity = SubCategoryDto, IsSuccess = true, Message = "SubCategory Updated Successfully" };
            }



        }

        //Delete
        public async Task<ResultView<CreateOrUpdateSubDto>> Delete(CreateOrUpdateSubDto subcategoryDto)
        {
            try
            {
                var Subcategory = await _subCategoryRepository.GetOneAsync(subcategoryDto.Id);
                if (Subcategory == null)
                {
                    return new ResultView<CreateOrUpdateSubDto> { Entity = null, IsSuccess = false, Message = "SubCategory Not Found!" };
                }

                await _subCategoryRepository.DeleteAsync(Subcategory);
                await _subCategoryRepository.SaveChangesAsync();

                var SubCategoryDto = _mapper.Map<CreateOrUpdateSubDto>(Subcategory);
                return new ResultView<CreateOrUpdateSubDto> { Entity = SubCategoryDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateSubDto> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }




        // GetAll
        public async Task<ResultDataForPagination<GetAllSubDto>> GetAll(int item, int pagnumber)
        {
            var AllData = await _subCategoryRepository.GetAllAsync();
            var SubCategorys = AllData.Skip(item * (pagnumber - 1)).Take(item)
             .Select(c => new GetAllSubDto
             {
                 Id = c.Id,
                 Name = c.Name,
                 Description = c.Description,
                 Image = c.Image,
                 CategoryName = c.Category.Name,



             }).ToList();

            ResultDataForPagination<GetAllSubDto> resultDataFor = new ResultDataForPagination<GetAllSubDto>();

            resultDataFor.Entities = SubCategorys;
            resultDataFor.count = AllData.Count();


            return resultDataFor;

        }




        //GetOne
        public async Task<ResultView<GetAllSubDto>> GetOne(int id)
        {
            var SubCategory = await _subCategoryRepository.GetOneAsync(id);
            if (SubCategory == null)
            {
                return new ResultView<GetAllSubDto> { Entity = null, IsSuccess = false, Message = "Not Found!" };
            }
            else
            {
                var SubCategoryDto = _mapper.Map<GetAllSubDto>(SubCategory);

                return new ResultView<GetAllSubDto> { Entity = SubCategoryDto, IsSuccess = true, Message = "Succses" };
            }
        }































    }
}
