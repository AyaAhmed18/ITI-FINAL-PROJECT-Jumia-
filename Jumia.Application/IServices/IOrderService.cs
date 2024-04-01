using Jumia.Dtos.Order;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IOrderService
    {
       Task<ResultView<CreateOrUpdateOrderDto>> Create(CreateOrUpdateOrderDto ordDTO);
        Task<ResultView<CreateOrUpdateOrderDto>> Update(CreateOrUpdateOrderDto ordDTO);
        Task<ResultView<CreateOrUpdateOrderDto>> HardDelete(int id);
        Task<ResultDataForPagination<GetAllOrdersDTO>> GetAllPagination(int items, int pagenumber);

        Task<List<GetAllOrdersDTO>> GetAllOrders();
        Task<CreateOrUpdateOrderDto> GetOrder(int id);
    }
}
