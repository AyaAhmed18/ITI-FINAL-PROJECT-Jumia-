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
        private readonly IOrderRepository _OrderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ResultView<CreateOrUpdateOrderDto>> Create(CreateOrUpdateOrderDto bookDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllOrdersDTO>> GetAllOrders()
        {
            try
            {
                if (_unitOfWork.OrderRepository != null)
                {
                    var allOrders = (await _OrderRepository.GetAllAsync());
                    List<GetAllOrdersDTO> orders = allOrders.Select(p => new GetAllOrdersDTO()
                    {
                        Id = p.Id,
                        Customer = p.Customer.UserName,
                        Status = p.Status.ToString(),
                        OrderDate = p.CreatedDate,
                        TotalPrice = p.TotalPrice,
                        Discount=p.Discount
                    }).ToList();

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

        public async Task<ResultDataForPagination<GetAllOrdersDTO>> GetAllPagination(int items, int pagenumber)
        {
            try
            {
                var AlldAta = (await _unitOfWork.OrderRepository.GetAllAsync());
                var Orders = AlldAta.Skip(items * (pagenumber - 1)).Take(items)
                                                  .Select(p => new GetAllOrdersDTO()
                                                  {
                                                      Id = p.Id,
                                                     
                                                  }).ToList();
                ResultDataForPagination<GetAllOrdersDTO> resultDataList = new ResultDataForPagination<GetAllOrdersDTO>();
                resultDataList.Entities = Orders;
                resultDataList.count = AlldAta.Count();
                return resultDataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }

        }
        public async Task<CreateOrUpdateOrderDto> GetOrder(int id)
        {
            try
            {
                var b = await _unitOfWork.OrderRepository.GetOneAsync(id);
                var REturnb = _mapper.Map<CreateOrUpdateOrderDto>(b);
                return REturnb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<ResultView<CreateOrUpdateOrderDto>> HardDelete(int id)
        {
            try
            {
                // var book = _mapper.Map<Book>(bookDTO);
                var existingBook = await _unitOfWork.OrderRepository.GetOneAsync(id);
                if (existingBook == null)
                {
                    return new ResultView<CreateOrUpdateOrderDto> { Entity = null, IsSuccess = false, Message = "Book not found" };
                }
                var Oldbook = _unitOfWork.OrderRepository.DeleteAsync(existingBook);
                await _unitOfWork.SaveChangesAsync();

                var bDto = _mapper.Map<CreateOrUpdateOrderDto>(Oldbook);
                return new ResultView<CreateOrUpdateOrderDto> { Entity = bDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateOrderDto> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }

        

        public async Task<ResultView<CreateOrUpdateOrderDto>> Update(CreateOrUpdateOrderDto bookDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(bookDTO);
                var updateOrder = await _unitOfWork.OrderRepository.UpdateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                var ODto = _mapper.Map<CreateOrUpdateOrderDto>(updateOrder);
                return new ResultView<CreateOrUpdateOrderDto> { Entity = ODto, IsSuccess = true, Message = "Updated Successfully" };

            }
            catch (Exception ex)
            {
                return new ResultView<CreateOrUpdateOrderDto> { Entity = null, IsSuccess = false, Message = "Something went wrong" };

                // Console.WriteLine($"An error occurred: {ex.Message}");
                //throw;
            }
        }
    }
}
