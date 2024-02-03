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
    public class UpdateCategoryCommand : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public UpdateCategoryCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category= await _context.Categories.FirstOrDefaultAsync(x=> x.Id == request.Id);
            category.Name=request.Name;
            
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<UpdateCategoryCommandResponse>(category);
        }
    }
}
