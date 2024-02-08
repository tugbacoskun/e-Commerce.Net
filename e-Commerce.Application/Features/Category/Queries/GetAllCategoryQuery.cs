using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;

namespace e_Commerce.Application.Features.Category.Queries
{
    public class GetAllCategoryQuery : IRequestHandler<GetAllCategoryRequestQuery, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRedisCacheService _redisCacheService;

        public GetAllCategoryQuery(IMapper mapper, IRedisCacheService redisCacheService, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _categoryRepository = categoryRepository;
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
