using AutoMapper;
using Jumia.Dtos.Brand;
using Jumia.Dtos.Category;
using Jumia.Dtos.Order;
using Jumia.Dtos.OrderItems;
using Jumia.Dtos.Product;
using Jumia.Dtos.Shippment;
using Jumia.Dtos.Specification;
using Jumia.Dtos.SubCategory;
using Jumia.Dtos.SubCategorySpecifications;
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
              //  .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.GetLocalized(src.NameAr,src.Name) ))
                .ReverseMap();
            CreateMap< Category , GetAllCategoryDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))
               

                .ReverseMap();
            CreateMap<GetAllCategoryDto, CreateOrUpdateCategoryDto>()
                .ReverseMap();

            //SubCategory
            CreateMap<SubCategory, CreateOrUpdateSubDto>().ReverseMap();
            CreateMap<SubCategorySpecification, CreateOrUpdateSubDto>().ReverseMap();
            CreateMap< SubCategory, GetAllSubDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))
                 .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<GetAllSubDto , CreateOrUpdateSubDto>().ReverseMap() ;
            CreateMap<Specification, GetAllSpecificationDto > ().ReverseMap();


            //User&Role
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();
            CreateMap<GetRole, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserRole>().ReverseMap();
            CreateMap<GetAllUsers, UserIdentity>().ReverseMap();

            //Order&OrderItmes
            CreateMap<GetAllOrdersDTO, Order>().ReverseMap();
             CreateMap<CreateOrUpdateOrderDto, Order>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap();
            CreateMap<GetAllOrderItemsDto, OrderItems>()
                
                .ReverseMap();
            CreateMap<CreatOrUpdateOrderItemsDto, OrderItems>().ReverseMap();

            //Shippment
           // CreateMap<Shippment, GetShippmentDto>().ReverseMap();
            CreateMap<Shippment, GetShippmentDto>()
             //   .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetLocalized(src.FirstNameAr, src.FirstNameEn)))
              //  .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetLocalized(src.LastNameAr, src.LastName)))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.GetLocalized(src.CityAr, src.City)))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.GetLocalized(src.RegionAr, src.Region)))

                .ReverseMap();
            CreateMap<Shippment, CreateOrUpdateShipmentDto >().ReverseMap();

            //Product
            CreateMap<Product, GetAllProducts>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))
                 .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.GetLocalized(src.LongDescription, src.ShortDescription)))

                .ReverseMap();
            CreateMap<Product,CreateOrUpdateProductDto>().ReverseMap();
            CreateMap<GetAllProducts, CreateOrUpdateProductDto>()
              .ReverseMap();

            //Specification
            CreateMap<Specification, GetAllSpecificationDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetLocalized(src.NameAr, src.Name)))
                .ReverseMap();
            CreateMap<SubCategorySpecification, CreateOrUpdateSubCategorySpecificationDto>().ReverseMap();
            CreateMap<SubCategorySpecification, GetAllSubCategorySpecificationDto>().ReverseMap();

            //Brand
            CreateMap<Brand,GetAllBrandDto>().ReverseMap();
        }

    }
}
