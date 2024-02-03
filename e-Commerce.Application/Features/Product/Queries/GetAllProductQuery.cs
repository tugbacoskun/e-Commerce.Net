using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Domain.Entities;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Queries
{
    public class GetAllProductQuery : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public GetAllProductQuery(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync();

            var productDtos = _mapper.Map< List<GetAllProductQueryResponse>>(products);
            
            return productDtos;

        }
    }
}
