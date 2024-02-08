using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Fluent_Validation;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class AddProductCommand : IRequestHandler<AddProductCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository  _productRepository;
        private readonly IRedisCacheService _redisCacheService;

        public AddProductCommand(IMapper mapper,IRedisCacheService redisCacheService, IProductRepository productRepository)
        {
            _mapper = mapper; 
            _redisCacheService = redisCacheService;
            _productRepository = productRepository;
        }

        public async Task<DataResult> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddProductFluentValidatior();
                var result = validator.Validate(request);
                var test = await _productRepository.GetByCurrencyType(Domain.Enum.CurrencyTypeLookup.TRY);
                if (result.IsValid)
                {
                    var product = _mapper.Map<Domain.Entities.Product>(request); 
                    await _productRepository.AddAsync(product);
                    var data = _mapper.Map<AddProductCommandResponse>(product);

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
