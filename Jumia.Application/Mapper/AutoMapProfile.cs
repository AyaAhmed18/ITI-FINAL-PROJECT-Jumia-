using AutoMapper;
using Jumia.Dtos.Order;
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
            CreateMap<GetAllOrdersDTO, Order>().ReverseMap();
        }
    }
}
