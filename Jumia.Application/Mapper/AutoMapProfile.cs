using AutoMapper;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.Dtos.SubCategory;
using Jumia.Dtos.User;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
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
            //Category
            CreateMap<CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<GetAllCategoryDto, Category>().ReverseMap();
            CreateMap<GetAllCategoryDto, CreateOrUpdateCategoryDto>().ReverseMap();

            //SubCategory
            CreateMap<CreateOrUpdateSubDto, SubCategory>().ReverseMap();
            CreateMap<GetAllSubDto, SubCategory>().ReverseMap();
            CreateMap<GetAllSubDto , CreateOrUpdateSubDto>().ReverseMap() ;

            //User&Role
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();
            CreateMap<GetRole, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();

            //Order&OrderItmes
            CreateMap<GetAllOrdersDTO, Order>().ReverseMap();
            CreateMap<CreateOrUpdateOrderDto, Order>().ReverseMap();
            CreateMap<GetAllOrderItemsDto, OrderItems>().ReverseMap();
            CreateMap<CreatOrUpdateOrderItemsDto, OrderItems>().ReverseMap();

        }
    }
}
