using AutoMapper;
using Jumia.Dtos.Category;
using Jumia.Dtos.SubCategory;
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
            //Category
            CreateMap<CreateOrUpdateCategoryDto, Category>().ReverseMap();
            CreateMap<GetAllCategoryDto, Category>().ReverseMap();
            CreateMap<GetAllCategoryDto, CreateOrUpdateCategoryDto>().ReverseMap();

            //SubCategory
            CreateMap<CreateOrUpdateSubDto, SubCategory>().ReverseMap();
            CreateMap<GetAllSubDto, SubCategory>().ReverseMap();
            CreateMap<GetAllSubDto , CreateOrUpdateSubDto>().ReverseMap() ;


        }
    }
}
