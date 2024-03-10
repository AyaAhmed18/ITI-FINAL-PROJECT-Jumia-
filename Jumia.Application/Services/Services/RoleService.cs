using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.Services
{
    public class RoleService : IRoleService
    {
            private readonly IUserRoleRepository _userRoleRepository;
            private readonly IMapper _mapper;

            public RoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
            {

            _userRoleRepository = userRoleRepository;
                _mapper = mapper;


            }

            public async Task<ResultDataForPagination<GetRole>> GetAll()
            {



                var Alldata = (await _userRoleRepository.GetAllAsync());
                var Roles = Alldata.Select(p => new GetRole()
                {
                    Name = p.Name,
                  Id = p.Id,
                }).ToList();
            ResultDataForPagination<GetRole> resultDataList = new ResultDataForPagination<GetRole>();
                resultDataList.Entities = Roles;

                return resultDataList;
            }
        public async Task<IQueryable<RoleUser>> GetUsername()
        {
          return  await _userRoleRepository.GetUsername();
        }
    }

}
