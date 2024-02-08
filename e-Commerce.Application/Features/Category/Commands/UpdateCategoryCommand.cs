using AutoMapper;
using e_Commerce.Application.Fluent_Validation;
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

namespace e_Commerce.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : IRequestHandler<UpdateCategoryCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRedisCacheService _redisCacheService;
        public UpdateCategoryCommand(IMapper mapper, IRedisCacheService redisCacheService, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _categoryRepository = categoryRepository;
        }

        public async Task<DataResult> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateCategoryFluentValidator();
                var result = validator.Validate(request);
                if (result.IsValid)
                {
                    var category = await _categoryRepository.GetByIdAsync(request.Id);
                    category.Name = request.Name;
                    category.UpdatedDate= DateTime.UtcNow;

                    await _categoryRepository.UpdateAsync(category);

                    var data = _mapper.Map<UpdateCategoryCommandResponse>(category);

                    await _redisCacheService.Clear("Category_" + category.Id);
                    await _redisCacheService.SetValueAsync("Category_" + category.Id, JsonSerializer.Serialize(category));

                    return new DataResult
                    {
                        Data = data,
                        IsSuccess = true,
                        Message = "İşlem Başarılı"
                    };
                }
                else
                {
                    return new DataResult
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = result.Errors
                    };
                }
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
