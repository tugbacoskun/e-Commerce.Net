using AutoMapper;
using e_Commerce.Application.Redis;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        private readonly IRedisCacheService _redisCacheService;

        public DeleteProductCommand(IMapper mapper, IeCommerceDbContext context, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _context = context;
            _redisCacheService = redisCacheService;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product= await _context.Products.FirstOrDefaultAsync(p=>p.Id==request.Id);
            product.IsDeleted=true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            await _redisCacheService.Clear("Product_" +  request.Id);

            return _mapper.Map<DeleteProductCommandResponse>(product);
        }
    }
}
