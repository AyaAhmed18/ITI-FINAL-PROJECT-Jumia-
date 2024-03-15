using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.Shippment;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class ShippmentService : IShippmentService
    {
        private readonly IShippmentRepository _shippmentRepository;
        private readonly IMapper _mapper;
        public ShippmentService(IShippmentRepository shippmentRepository, IMapper mapper)
        {
            _shippmentRepository = shippmentRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<CreateOrUpdateShipmentDto>> Create(CreateOrUpdateShipmentDto shipmentDto)
        {
            try
            {
                
            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        public Task<ResultView<CreateOrUpdateShipmentDto>> Delete(CreateOrUpdateShipmentDto shipmentDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetShippmentDto>> GetAll()
        {
            try
            {
                if (_shippmentRepository != null)
                {
                    var allShippingData = (await _shippmentRepository.GetAllAsync());
                    List<GetShippmentDto> allData = allShippingData.Select(p => new GetShippmentDto()
                    {
                        Id= p.Id,
                        FirstName=p.FirstName,
                        LastName=p.LastName,
                        PhoneNumber=p.PhoneNumber,
                        Address=p.Address,
                        AdressInformation=p.AdressInformation,
                        City=p.City,
                        Region=p.Regin,
                        Cost=p.Cost,
                        OrderId=p.OrderId
                    }).ToList();

                    return allData;
                }
                else
                {
                    return new List<GetShippmentDto>();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<ResultView<CreateOrUpdateShipmentDto>> GetOne(int Id)
        {
            try
            {
                var shipping = await _shippmentRepository.GetOneAsync(Id);
                if (shipping == null)
                {
                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = null, IsSuccess = false, Message = "Not Found!" };
                }
                else
                {
                    var shippingDto = _mapper.Map<CreateOrUpdateShipmentDto>(shipping);

                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = shippingDto, IsSuccess = true, Message = "Succses" };
                }
            

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public Task<ResultView<CreateOrUpdateShipmentDto>> Update(CreateOrUpdateShipmentDto shipmentDto)
        {
            throw new NotImplementedException();
        }
    }
}
