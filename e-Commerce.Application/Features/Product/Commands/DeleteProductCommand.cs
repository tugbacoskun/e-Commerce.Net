using AutoMapper;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public DeleteProductCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product= await _context.Products.FirstOrDefaultAsync(p=>p.Id==request.Id);
            product.IsDeleted=true;
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DeleteProductCommandResponse>(product);
        }
    }
}
