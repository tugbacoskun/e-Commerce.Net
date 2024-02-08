using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequestHandler<DeleteProductCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IRedisCacheService _redisCacheService;

        public DeleteProductCommand(IMapper mapper,IRedisCacheService redisCacheService, IProductRepository productRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _productRepository = productRepository;
        }

        public async Task<DataResult> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var product = await _productRepository.GetByIdAsync(request.Id);
                product.IsDeleted = true;
                await _productRepository.UpdateAsync(product);

                await _redisCacheService.Clear("Product_" + request.Id);

               var id= _mapper.Map<DeleteProductCommandResponse>(request.Id);
                return new DataResult
                {
                    Data = id,
                    IsSuccess = true,
                    Message = "İşlem Başarılı"
                };
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
