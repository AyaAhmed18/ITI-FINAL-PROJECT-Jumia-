using AutoMapper;
using Jumia.Dtos.User;
using Jumia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Mapper
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {

            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();
            CreateMap<GetRole, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();

        }
    }
}
