using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Dtos.Order;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Jumia.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<GetAllOrdersDTO>> GetAllOrders()
        {
            try
            {
                if (_unitOfWork.OrderRepository != null)
                {
                    var allOrders = (await _unitOfWork.OrderRepository.GetAllAsync());
                    List<GetAllOrdersDTO> orders = allOrders.Select(p => new GetAllOrdersDTO()
                    {
                        Id = p.Id,
                        Customer = p.Customer.UserName,
                        Status = p.Status,
                        ShippingStatus = p.Shipping.DelivaryWay,
                        PaymentStatus = p.payment.Type,
                        OrderDate = p.OrderDate,
                        TotalPrice = p.TotalPrice,
                    }).ToList();

                    //   var orders1 = allOrders.ToList();
                    return orders;
                }
                else
                {
                    return new List<GetAllOrdersDTO>();
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }

        public Task<ResultDataForPagination<GetAllOrdersDTO>> GetAllPagination(int items, int pagenumber)
        {
            throw new NotImplementedException();
        }
        public async Task<CreateOrUpdateOrderDto> GetOrder(int id)
        {
            var b = await _unitOfWork.OrderRepository.GetOneAsync(id);
            var REturnb = _mapper.Map<CreateOrUpdateOrderDto>(b);
            return REturnb;
        }
    }
}
