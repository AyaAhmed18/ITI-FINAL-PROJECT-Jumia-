using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Dtos.Product;
using Jumia.Dtos.Specification;
using Jumia.Dtos.SubCategory;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class SpecificationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SpecificationServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultDataForPagination<GetAllSpecificationDto>> GetAllPagination() //10 , 3 -- 20 30
        {
            var AlldAta = (await _unitOfWork.SpecificationRepository.GetAllAsync());
            var specifications = AlldAta.ToList();
            var Specifications = _mapper.Map<List<GetAllSpecificationDto>>(specifications);
            ResultDataForPagination<GetAllSpecificationDto> resultDataList = new ResultDataForPagination<GetAllSpecificationDto>();
            resultDataList.Entities = Specifications;
            //resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
    }
}
