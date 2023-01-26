﻿using AutoMapper;
using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;

namespace ECommerce.Services.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Category,CategoryDto>().ReverseMap();
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<Feature,FeatureDto >().ReverseMap();

            CreateMap<Course,CourseCreateDto>().ReverseMap();
            CreateMap<Course,CourseUpdateDto>().ReverseMap();
        }
    }
}
