using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IOrderItemService
    {
        Task<CreatOrUpdateOrderItemsDto> GetOrderItems(int id);
        Task<List<GetAllOrderItemsDto>> GetAllOrderItems();
        Task<ResultView<CreatOrUpdateOrderItemsDto>> HardDelete(int id);
    }
}
