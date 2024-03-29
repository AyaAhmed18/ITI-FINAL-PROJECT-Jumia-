using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Brand;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class BrandService:IBrandService
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _prandrepository;

        public BrandService(IBrandRepository prandrepository, IMapper mapper)
        {
            _mapper = mapper;

            _prandrepository = prandrepository;
        }
        public async Task<ResultDataForPagination<GetAllBrandDto>> GetAll()
        {
            var AllData = (await _prandrepository.GetAllAsync());
            var Brand = AllData.ToList();
            var Brands = _mapper.Map<List<GetAllBrandDto>>(Brand);

            ResultDataForPagination<GetAllBrandDto> resultDataFor = new ResultDataForPagination<GetAllBrandDto>();

            resultDataFor.Entities = Brands;

            return resultDataFor;

        }
    }
}
