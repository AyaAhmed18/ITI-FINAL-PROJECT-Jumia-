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
      //  Task<ResultView<CreateOrUpdateBookDTO>> Create(CreateOrUpdateBookDTO bookDTO);
       // Task<ResultView<CreateOrUpdateBookDTO>> Update(CreateOrUpdateBookDTO bookDTO);
      //  Task<ResultView<CreateOrUpdateBookDTO>> HardDelete(int id);
      //  Task<ResultView<CreateOrUpdateBookDTO>> SoftDelete(int ids);
        Task<ResultDataForPagination<GetAllOrdersDTO>> GetAllPagination(int items, int pagenumber);

        Task<List<GetAllOrdersDTO>> GetAllOrders();
        Task<CreateOrUpdateOrderDto> GetOrder(int id);
    }
}
