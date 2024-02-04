using AutoMapper;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public DeleteCategoryCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                bool any = await _context.Products.AnyAsync(x => x.CategoryId == request.Id);
                if (any)
                    return null;

                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return _mapper.Map<DeleteCategoryCommandResponse>(category.Id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
