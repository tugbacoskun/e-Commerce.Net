using AutoMapper;
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
        private readonly IeCommerceDbContext _context;
        private readonly IRedisCacheService _redisCacheService;
        public DeleteCategoryCommand(IMapper mapper, IeCommerceDbContext context, IRedisCacheService redisCacheService)
        {
            _mapper = mapper;
            _context = context;
            _redisCacheService = redisCacheService;
        }

        public async Task<DataResult> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                bool any = await _context.Products.AnyAsync(x => x.CategoryId == request.Id);
                if (any)
                    return new DataResult
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Silmek istediğiniz kategoriye ait ürün(ler) var."
                    };

                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
                category.IsDeleted = true;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync(cancellationToken);

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
