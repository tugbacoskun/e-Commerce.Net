using AutoMapper;
using e_Commerce.Application.Fluent_Validation;
using e_Commerce.Application.Response;
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
    public class UpdateCategoryCommand : IRequestHandler<UpdateCategoryCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public UpdateCategoryCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DataResult> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateCategoryFluentValidator();
                var result = validator.Validate(request);
                if (result.IsValid)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
                    category.Name = request.Name;

                    _context.Categories.Update(category);
                    await _context.SaveChangesAsync();

                    var data = _mapper.Map<UpdateCategoryCommandResponse>(category);

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
