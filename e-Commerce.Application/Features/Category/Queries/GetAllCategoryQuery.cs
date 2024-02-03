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
    public class GetAllCategoryQuery : IRequestHandler<GetAllCategoryRequestQuery, List<GetAllCategoryResponseQuery>>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public GetAllCategoryQuery(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


       public async Task<List<GetAllCategoryResponseQuery>> Handle(GetAllCategoryRequestQuery request, CancellationToken cancellationToken)
        {
             var categories= _context.Categories.ToListAsync();
            var categoryDtos= _mapper.Map<List<GetAllCategoryResponseQuery>>(categories);
            return categoryDtos;
        }
    }
}
