using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Redis;
using e_Commerce.Domain.Entities;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Queries
{
    public class GetByIdProductQueries: IRequestHandler<GetByIdProductQueriesRequest, GetByIdProductQueriesResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _dbContext;
        private readonly IRedisCacheService _redisCacheService;
        public GetByIdProductQueries(IMapper mapper, IeCommerceDbContext dbContext, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _redisCacheService = redisCacheService;
        }


        public async Task<GetByIdProductQueriesResponse> Handle(GetByIdProductQueriesRequest request, CancellationToken cancellationToken)
        {


            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

            var productDto = _mapper.Map<GetByIdProductQueriesResponse>(product);

            await _redisCacheService.GetValueAsync("Product_" + product.Id);

            return productDto;
        }
    }
}
