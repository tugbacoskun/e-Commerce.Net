using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Application.Features.Category.Queries;
using e_Commerce.Application.Features.Product.Queries;
using e_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Mapping
{
    public class Mapping: Profile
    {
        public Mapping() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryResponseQuery>().ReverseMap();
            CreateMap<Category, GetAllCategoryResponseQuery>().ReverseMap(); 
            CreateMap<Category, AddCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, AddCategoryCommandResponse>().ReverseMap();
            CreateMap<List<Category>, List<GetAllCategoryResponseQuery>>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, GetByIdProductQueriesResponse>().ReverseMap();
            CreateMap<Product, GetAllProductQueryResponse>().ReverseMap();
        }    
    }
}
