using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Queries
{
    public class GetByIdCategoryQuery : IRequestHandler<GetByIdCategoryRequestQuery, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRedisCacheService _redisCacheService;

        public GetByIdCategoryQuery(IMapper mapper, IeCommerceDbContext context, IRedisCacheService redisCacheService, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _categoryRepository = categoryRepository;
        }

        public async Task<DataResult> Handle(GetByIdCategoryRequestQuery request, CancellationToken cancellationToken)
        {
            var redisCategory = await _redisCacheService.GetValueAsync("Category_" + request.Id);
            if (redisCategory != null)
            {
                return new DataResult
                {
                    Data = redisCategory,
                    IsSuccess = true,
                    Message = "İşlem başarılı"
                };
            }

            var category = await _categoryRepository.GetByIdAsync(request.Id);



            if (category != null)
            {
                await _redisCacheService.SetValueAsync("Category_" + request.Id, JsonSerializer.Serialize(category));
                var categoryDto = _mapper.Map<GetByIdCategoryResponseQuery>(category);

                return new DataResult
                {
                    Data = categoryDto,
                    IsSuccess = true,
                    Message = "İşlem başarılı"
                };
            }

            else
            {
                return new DataResult
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Kategori Bulunamadı"
                };
            }



        }
    }
}
