using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Order;
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

        public Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Delete(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<CreateOrUpdateSubCategorySpecificationDto>> Update(CreateOrUpdateSubCategorySpecificationDto subCategorySpecificationDto)
        {
            throw new NotImplementedException();
        }
    }
}
