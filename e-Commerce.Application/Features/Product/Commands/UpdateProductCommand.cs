using AutoMapper;
using e_Commerce.Application.Fluent_Validation;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequestHandler<UpdateProductCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IRedisCacheService _redisCacheService;

        public UpdateProductCommand(IMapper mapper, IRedisCacheService redisCacheService, IProductRepository productRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _productRepository = productRepository;
        }

        public async Task<DataResult> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateProductFluentValidatior();
                var result = validator.Validate(request);
                if (result.IsValid)
                {
                    var product = await _productRepository.GetByIdAsync(request.Id);
                    product.ProductCurrency = request.ProductCurrency;
                    product.Price = request.Price;
                    product.CategoryId = request.CategoryId;
                    product.Description = request.Description;
                    product.Image = request.Image;
                    product.Name = request.Name;


                   await _productRepository.UpdateAsync(product);

                    var data = _mapper.Map<UpdateProductCommandResponse>(product);

                    await _redisCacheService.Clear("Product_" + product.Id);
                    await _redisCacheService.SetValueAsync("Product_" + product.Id, JsonSerializer.Serialize(product));

                    return new DataResult
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "İşlem Başarılı"
                    };
                }
                else
                {
                    return new DataResult
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = result.Errors
                    };
                }
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }

        }
    }
}
