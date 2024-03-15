using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.IServices
{
    public interface  IRoleService
    {
        Task<ResultDataForPagination<GetRole>> GetAll();
        Task<IQueryable<RoleUser>> GetUsername();
    }
}
