using AutoMapper;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Queries
{
    public class GetByIdCategoryQuery : IRequestHandler<GetByIdCategoryRequestQuery, GetByIdCategoryResponseQuery>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public GetByIdCategoryQuery(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetByIdCategoryResponseQuery> Handle(GetByIdCategoryRequestQuery request, CancellationToken cancellationToken)
        {
            var category= await _context.Categories.FirstOrDefaultAsync(p=>p.Id==request.Id);
            var categoryDto = _mapper.Map<GetByIdCategoryResponseQuery>(category);
            return categoryDto;
        }
    }
}
