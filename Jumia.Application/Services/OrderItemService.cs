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

        public async Task<List<GetAllOrderItemsDto>> GetAllOrderItems()
        {
            try
            {
                if (_unitOfWork.OrderRepository != null)
                {
                    var items = (await _unitOfWork.OrderItemsRepository.GetAllAsync());
                    List<GetAllOrderItemsDto> item = items.Select(p => new GetAllOrderItemsDto()
                    {
                        OrderId = p.OrderId,
                        ProductImage = p.Product.Images.ToString(), /////
                        ProductName = p.Product.Name,
                        ProductQuantity=p.ProductQuantity,
                        TotalPrice = p.TotalPrice,
                        Discount = p.Discount
                    }).ToList();

                    return item;
                }
                else
                {
                    return new List<GetAllOrderItemsDto>();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<CreatOrUpdateOrderItemsDto> GetOrderItems(int id)
        {
            try { 
            var b = await _unitOfWork.OrderItemsRepository.GetOneAsync(id);
            var REturnb = _mapper.Map<CreatOrUpdateOrderItemsDto>(b);
            return REturnb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        
        }
}
