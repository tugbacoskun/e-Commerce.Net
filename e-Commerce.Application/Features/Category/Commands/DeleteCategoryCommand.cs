using AutoMapper;
using e_Commerce.Application.Interfaces;
using e_Commerce.Application.Redis;
using e_Commerce.Application.Response;
using e_Commerce.Domain.Entities;
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
    public class DeleteCategoryCommand : IRequestHandler<DeleteCategoryCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRedisCacheService _redisCacheService;
        public DeleteCategoryCommand(IMapper mapper, IRedisCacheService redisCacheService, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _categoryRepository = categoryRepository;
        }

        public async Task<DataResult> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
               var category = await _categoryRepository.GetByIdAsync(request.Id);
                if (category !=null)

                    return new DataResult
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Silmek istediğiniz kategoriye ait ürün(ler) var."
                    };

                var category2 = await _categoryRepository.GetByIdAsync(request.Id);
                category.IsDeleted = true;
                await _categoryRepository.UpdateAsync(category);

                await _redisCacheService.Clear("Category_" + category.Id);

                var id= _mapper.Map<DeleteCategoryCommandResponse>(request.Id);

                return new DataResult
                {
                    Data = id,
                    IsSuccess = true,
                    Message =  "Kategori silme işleminiz başarıyla tamamlandı."
                };
            }
            catch (Exception ex)
            {
                return new DataResult
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }
            
        }
    }
}
