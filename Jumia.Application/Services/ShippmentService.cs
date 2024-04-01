using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.Shippment;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Localization.Shared_Recources;
using Microsoft.Extensions.Localization;
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
       // private readonly IStringLocalizer<SharedRecources> _stringLocalizer;
        public ShippmentService(IShippmentRepository shippmentRepository, IMapper mapper)
        {
            _shippmentRepository = shippmentRepository;
            _mapper = mapper;
          //  _stringLocalizer = stringLocalizer;
        }
        public async Task<ResultView<CreateOrUpdateShipmentDto>> Create(CreateOrUpdateShipmentDto shipmentDto)
        {
            try
            {
                var Data = await _shippmentRepository.GetAllAsync();
                var OldShip = Data.Where(c => c.Id == shipmentDto.Id).FirstOrDefault();

                if (OldShip != null)
                {
                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = null, IsSuccess = false, Message = "this Address Already Exist" };
                }
                else
                {
                    var ship = _mapper.Map<Shippment>(shipmentDto);
                    var NewShip = await _shippmentRepository.CreateAsync(ship);
                    await _shippmentRepository.SaveChangesAsync();
                    var ShDto = _mapper.Map<CreateOrUpdateShipmentDto>(NewShip);


                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = ShDto, IsSuccess = true, Message = "User Address Added Successfully" };
                }

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateShipmentDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
            }
        
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
                    var allShippingData = (await _shippmentRepository.GetAllAsync()).ToList();
                    List<GetShippmentDto> allData = _mapper.Map<List<GetShippmentDto>>(allShippingData);
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

                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = shippingDto, IsSuccess = true, Message = "Success" };
                }
            

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<ResultView<CreateOrUpdateShipmentDto>> Update(CreateOrUpdateShipmentDto shipmentDto)
        {
            try
            {
                var Data = await _shippmentRepository.GetOneAsync(shipmentDto.Id);

                if (Data == null)
                {
                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = null, IsSuccess = false, Message = "This User Address Data Not Found!" };

                }
                else
                {
                    var shipp = _mapper.Map<Shippment>(shipmentDto);
                    var shipEdit = await _shippmentRepository.UpdateAsync(shipp);
                    await _shippmentRepository.SaveChangesAsync();
                    var shipDto = _mapper.Map<CreateOrUpdateShipmentDto>(shipEdit);

                    return new ResultView<CreateOrUpdateShipmentDto> { Entity = shipDto, IsSuccess = true, Message = "User Address Date Updated Successfully" };
                }

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateShipmentDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = $"Something went wrong: {ex.Message}"
                };
                // Console.WriteLine($"An error occurred: {ex.Message}");
                //throw;
            }
        }
    }
}
