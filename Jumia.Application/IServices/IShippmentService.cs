using Jumia.Dtos.Category;
using Jumia.Dtos.Shippment;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.IServices
{
    public interface IShippmentService
    {
        Task<ResultView<CreateOrUpdateShipmentDto>> Create(CreateOrUpdateShipmentDto shipmentDto);

        //Update
        Task<ResultView<CreateOrUpdateShipmentDto>> Update(CreateOrUpdateShipmentDto shipmentDto);

        // Delete
        Task<ResultView<CreateOrUpdateShipmentDto>> Delete(CreateOrUpdateShipmentDto shipmentDto);

        //GetOne
        Task<ResultView<CreateOrUpdateShipmentDto>> GetOne(int Id);

        //GetAll
        Task<List<GetShippmentDto>> GetAll();
    }
}
