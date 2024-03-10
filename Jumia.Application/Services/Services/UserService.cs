using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository , IMapper mapper) {

            _userRepository =   userRepository;
            _mapper = mapper;


        }

        public async Task<ResultView<GetAllUsers>> CreateAsync( GetAllUsers getAllUsers)
        {
            try
            {
                var Query = (await _userRepository.GetAllAsync());
                var Olduser = Query.Where(i => i.Id == getAllUsers.Id).FirstOrDefault();
                if (Olduser != null)
                {
                    return new ResultView<GetAllUsers> { Entity = null, IsSuccess = false, Message = "arready exsit" };
                }
                else
                {
                    var User = _mapper.Map<UserIdentity>(getAllUsers);
                    var NewUser = await _userRepository.CreateAsync(User);
                    await _userRepository.SaveChangesAsync();
                    var UserDto = _mapper.Map<GetAllUsers>(NewUser);
                    return new ResultView<GetAllUsers> { Entity = UserDto, IsSuccess = true, Message = "Add Success" };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error");
            }
        }

        public async Task<ResultDataForPagination<GetAllUsers>> GetAll()
        {


            var Alldata = (await _userRepository.GetAllAsync());
            var Users = Alldata.Select(p => new GetAllUsers()
            {
                 UserName = p.UserName,
                             
            }).ToList();
            ResultDataForPagination<GetAllUsers> resultDataList = new ResultDataForPagination<GetAllUsers>();
            resultDataList.Entities = Users;
        
            return   resultDataList;
        }
    }
}

