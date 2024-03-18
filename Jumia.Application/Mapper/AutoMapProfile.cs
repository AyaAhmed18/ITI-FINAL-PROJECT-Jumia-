using AutoMapper;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.Dtos.Shippment;
using Jumia.Dtos.SubCategory;
using Jumia.Dtos.User;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumia.Application.Mapper
{
    public class AutoMapProfile : Profile
    {
        /*private string GetLocalized(string textAr, string textEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return textEn;
        }*/
        public AutoMapProfile()
        {
            //Category
            CreateMap<Category , CreateOrUpdateCategoryDto>()
               // .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.GetLocalized(src.NameAr,src.Name) ))
                .ReverseMap();
            CreateMap< Category , GetAllCategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))
                .ReverseMap();
            CreateMap<GetAllCategoryDto, CreateOrUpdateCategoryDto>()
                .ReverseMap();

            //SubCategory
            CreateMap<SubCategory, CreateOrUpdateSubDto >()
              //  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))

                .ReverseMap();
            CreateMap< SubCategory, GetAllSubDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))

                .ReverseMap();
            CreateMap<GetAllSubDto , CreateOrUpdateSubDto>().ReverseMap() ;

            //User&Role
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();
            CreateMap<GetRole, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();

            //Order&OrderItmes
            CreateMap<GetAllOrdersDTO, Order>().ReverseMap();
             CreateMap<CreateOrUpdateOrderDto, Order>()
                .ReverseMap();
            CreateMap<GetAllOrderItemsDto, OrderItems>()
                
                .ReverseMap();
            CreateMap<CreatOrUpdateOrderItemsDto, OrderItems>().ReverseMap();

            //Shippment
           // CreateMap<Shippment, GetShippmentDto>().ReverseMap();
            CreateMap<Shippment, GetShippmentDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetLocalized(src.FirstNameAr, src.FirstNameEn)))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetLocalized(src.LastNameAr, src.LastName)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.GetLocalized(src.CityAr, src.City)))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.GetLocalized(src.RegionAr, src.Region)))

                .ReverseMap();
            CreateMap<Shippment, CreateOrUpdateShipmentDto >().ReverseMap();


        }
       
    }
}
