using AutoMapper;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using Hangfire.MemoryStorage.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : IRequestHandler<GetAllCategoryRequestQuery, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        private readonly IRedisCacheService _redisCacheService;

        public GetAllCategoryQuery(IMapper mapper, IeCommerceDbContext context, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _context = context;
            _redisCacheService = redisCacheService;
        }


        public async Task <DataResult> Handle(GetAllCategoryRequestQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _redisCacheService.GetAllValuesStartingWithAsync("Category_");

            var categoryDtos= _mapper.Map<List<GetAllCategoryResponseQuery>>(categoryList);

            return new DataResult
            {
                Data = categoryDtos,
                IsSuccess = true,
                Message= "İşlem başarılı"
            };
        }
    }
}
