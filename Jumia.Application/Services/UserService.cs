using AutoMapper;
using Jumia.Application.Contract;
using Jumia.Application.Services.IServices;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.User;
using Jumia.DTOS.ViewResultDtos;
using Jumia.Model;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserRoleRepository  _userRoleRepository;
        public UserService(IUserRepository userRepository , IMapper mapper , IUserRoleRepository userRoleRepository) {

            _userRepository =   userRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;

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
                    var userRole = _mapper.Map<UserRole>(getAllUsers);
                    await _userRoleRepository.CreateAsync(userRole);
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

        // delete
        public async Task<ResultView<GetAllUsers>> Delete(GetAllUsers getAllUsers)
        {
            try
            {
                var category = await _userRepository.GetOneAsync(getAllUsers.Id);
                if (category == null)
                {
                    return new ResultView<GetAllUsers> { Entity = null, IsSuccess = false, Message = "User Not Found!" };
                }

                var res = await _userRepository.DeleteAsync(category);
                var res1 = await _userRepository.SaveChangesAsync();

                var CategoryDto = _mapper.Map<GetAllUsers>(res);
                return new ResultView<GetAllUsers> { Entity = getAllUsers, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<GetAllUsers> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }


        //GetOne
        public async Task<ResultView<GetAllUsers>> GetOne(int id)
        {
            var user = await _userRepository.GetOneAsync(id);
            if (user == null)
            {
                return new ResultView<GetAllUsers> { Entity = null, IsSuccess = false, Message = "Not Found!" };
            }
            else
            {
                var UserDto = _mapper.Map<GetAllUsers>(user);

                return new ResultView<GetAllUsers> { Entity = UserDto, IsSuccess = true, Message = "Succses" };
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

