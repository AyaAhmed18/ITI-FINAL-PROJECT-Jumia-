using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<List<GetAllOrderItemsDto>> GetAllOrderItems()
        {
            throw new NotImplementedException();
        }

        public async Task<CreatOrUpdateOrderItemsDto> GetOrderItems(int id)
        {
            var b = await _unitOfWork.OrderItemsRepository.GetOneAsync(id);
            var REturnb = _mapper.Map<CreatOrUpdateOrderItemsDto>(b);
            return REturnb;
        }

        public async Task<ResultView<CreatOrUpdateOrderItemsDto>> Update(CreatOrUpdateOrderItemsDto orderItemDto)
        {
            var b = _mapper.Map<OrderItems>(orderItemDto);
            try
            {
                var updateorderStatus = await _unitOfWork.OrderItemsRepository.UpdateAsync(b);
                await _unitOfWork.SaveChangesAsync();
                var orderitemDTO = _mapper.Map<CreatOrUpdateOrderItemsDto>(updateorderStatus);
                return new ResultView<CreatOrUpdateOrderItemsDto> { Entity = orderitemDTO, IsSuccess = true, Message = "Created Successfully" };

            }
            catch (Exception ex)
            {
                return new ResultView<CreatOrUpdateOrderItemsDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        }
}
