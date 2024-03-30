using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Dtos.Specification;
using Jumia.Dtos.SubCategorySpecifications;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class SubCategorySpecificationService : ISubCategorySpecificationsService
    {
        private readonly ISubCategorySpecificationRepository _subCategorySpecificationRepository;
        private readonly IMapper _mapper;
        public SubCategorySpecificationService(ISubCategorySpecificationRepository subCategorySpecificationRepository, IMapper mapper)
        {
            _subCategorySpecificationRepository = subCategorySpecificationRepository;
            _mapper = mapper;
            //  _stringLocalizer = stringLocalizer;
        }
        public async Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Create(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto)
        {
            try
            {
                var Data = await _subCategorySpecificationRepository.GetAllAsync();
                var OldSpec = Data.Where(c => c.Id == subCategorySpecificationDto.Id).FirstOrDefault();

                if (OldSpec != null)
                {
                    return new ResultView<CreateOrUpdateSubCategorySpecificationDto> { Entity = null, IsSuccess = false, Message = "This Specification Already Exist in this SubCategory" };
                }
                else
                {
                    var Spec = _mapper.Map<SubCategorySpecification>(subCategorySpecificationDto);
                    var NewSpec = await _subCategorySpecificationRepository.CreateAsync(Spec);
                    await _subCategorySpecificationRepository.SaveChangesAsync();
                    var specDto = _mapper.Map<CreateOrUpdateSubCategorySpecificationDto>(NewSpec);
                    return new ResultView<CreateOrUpdateSubCategorySpecificationDto> { Entity = specDto, IsSuccess = true, Message = "Order Created Successfully" };
                }

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateSubCategorySpecificationDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        }

        public async Task<ResultView<GetAllSubCategorySpecificationDto>> Delete(int id )
        {
            try
            {
                // var book = _mapper.Map<Book>(bookDTO);
                var existingSubCat = await _subCategorySpecificationRepository.GetOneAsync(id);
                if (existingSubCat == null)
                {
                    return new ResultView<GetAllSubCategorySpecificationDto> { Entity = null, IsSuccess = false, Message = "spec not found" };
                }
                var OldSubSpec = _subCategorySpecificationRepository.DeleteAsync(existingSubCat);
                await _subCategorySpecificationRepository.SaveChangesAsync();

                var bDto = _mapper.Map<GetAllSubCategorySpecificationDto>(OldSubSpec);
                return new ResultView<GetAllSubCategorySpecificationDto> { Entity = bDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<GetAllSubCategorySpecificationDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        public async Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Update(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto)
        {
            try
            {
                var Data = await _subCategorySpecificationRepository.GetOneAsync(subCategorySpecificationDto.Id);

                if (Data == null)
                {
                    return new ResultView<CreateOrUpdateSubCategorySpecificationDto> { Entity = null, IsSuccess = false, Message = "Spec Not Found!" };

                }
                else
                {
                    var spec = _mapper.Map<SubCategorySpecification>(subCategorySpecificationDto);
                    var ospecEdit = await _subCategorySpecificationRepository.UpdateAsync(spec);
                    await _subCategorySpecificationRepository.SaveChangesAsync();
                    var specDto = _mapper.Map<CreateOrUpdateSubCategorySpecificationDto>(ospecEdit);

                    return new ResultView<CreateOrUpdateSubCategorySpecificationDto> { Entity = specDto, IsSuccess = true, Message = "Spec Updated Successfully" };
                }

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateSubCategorySpecificationDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
                // Console.WriteLine($"An error occurred: {ex.Message}");
                //throw;
            }
        }

        public async Task<List<GetAllSubCategorySpecificationDto>> GetAll() //10 , 3 -- 20 30
        {
            var AlldAta = (await _subCategorySpecificationRepository.GetAllAsync()).Include(s=> s.Specification);
            List<GetAllSubCategorySpecificationDto> Specifications = AlldAta.Select(p => new GetAllSubCategorySpecificationDto()
            {
                Id = p.Id,
                SubCategoryId = p.SubCategoryId,
                specificationId = p.specificationId,
                SpecificationName=p.Specification.Name,
                SubCategoryName=p.SubCategory.Name
                
            }).ToList();
            return Specifications;
        }
    }
}
