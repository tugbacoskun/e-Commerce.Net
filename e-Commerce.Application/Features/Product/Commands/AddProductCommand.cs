using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class AddProductCommand : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public AddProductCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<e_Commerce.Domain.Entities.Product>(request);
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddProductCommandResponse>(product);
        }
    }
}
