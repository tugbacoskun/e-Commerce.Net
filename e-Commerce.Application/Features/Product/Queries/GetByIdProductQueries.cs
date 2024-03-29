﻿using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace e_Commerce.Application.Features.Product.Queries
{
    public class GetByIdProductQueries: IRequestHandler<GetByIdProductQueriesRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IRedisCacheService _redisCacheService;
        public GetByIdProductQueries(IMapper mapper, IRedisCacheService redisCacheService, IProductRepository productRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _productRepository = productRepository;
        }


        public async Task<DataResult> Handle(GetByIdProductQueriesRequest request, CancellationToken cancellationToken)
        {
            var redisProduct= await _redisCacheService.GetValueAsync("Product_" + request.Id);

            if (redisProduct != null)
            {
                return new DataResult
                {
                    Data = redisProduct,
                    IsSuccess = true,
                    Message = "İşlem başarılı"
                };
            }

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product != null)
            {
                await _redisCacheService.SetValueAsync("Product_" + request.Id, JsonSerializer.Serialize(product));
                var productDto = _mapper.Map<GetByIdProductQueriesResponse>(product);

                return new DataResult
                {
                    Data = productDto,
                    IsSuccess = true,
                    Message = "İşlem başarılı"
                };
            }

            else
            {
                return new DataResult
                {
                    Data = null,
                    IsSuccess = true,
                    Message = "Ürün Bulunamadı"
                };
            }
        }
    }
}
