using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Domain.Entities;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Queries
{
    public class GetAllProductQuery : IRequestHandler<GetAllProductQueryRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        private readonly IRedisCacheService _redisCacheService;
        public GetAllProductQuery(IMapper mapper, IeCommerceDbContext context, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _context = context;
            _redisCacheService = redisCacheService;
        }


        public async Task<DataResult> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var productList = await _redisCacheService.GetAllValuesStartingWithAsync("Product_"); 

 
            var productDtos = _mapper.Map<List<GetAllProductQueryResponse>>(productList);

            return new DataResult
            {
                Data = productDtos,
                IsSuccess = true,
                Message = "İşlem başarılı"
            };
        }

       
    }
}
