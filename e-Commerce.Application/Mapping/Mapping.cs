using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Application.Features.Category.Queries;
using e_Commerce.Application.Features.Product.Commands;
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
            CreateMap<Category, GetAllCategoryResponseQuery>();

            CreateMap<Category, AddCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, AddCategoryCommandResponse>().ReverseMap();

            CreateMap<Category, DeleteCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, DeleteCategoryCommandResponse>().ReverseMap();

            CreateMap<Category, UpdateCategoryCommandRequest>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommandResponse>().ReverseMap();


            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Product, AddProductCommandRequest>().ReverseMap();
            CreateMap<Product, AddProductCommandResponse>().ReverseMap();

            CreateMap<Product, GetByIdProductQueriesResponse>().ReverseMap();
            CreateMap<Product, GetAllProductQueryResponse>().ReverseMap();

            CreateMap<Product, DeleteProductCommandRequest>().ReverseMap();
            CreateMap<Product, DeleteProductCommandResponse>().ReverseMap();

            CreateMap<Product, UpdateProductCommandRequest>().ReverseMap();
            CreateMap<Product, UpdateProductCommandResponse>().ReverseMap();
        }    
    }
}
