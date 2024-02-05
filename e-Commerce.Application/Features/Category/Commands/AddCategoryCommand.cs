using AutoMapper;
using e_Commerce.Application.Dtos;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace e_Commerce.Application.Features.Category.Commands
{
    public class AddCategoryCommand : IRequestHandler<AddCategoryCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public AddCategoryCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DataResult> Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddCategoryFluentValidatior();
                var result = validator.Validate(request);
                if (result.IsValid)
                {
                    var category = _mapper.Map<Domain.Entities.Category>(request);
                    await _context.AddAsync(category, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var data = _mapper.Map<AddCategoryCommandResponse>(category);

                    return new DataResult
                    {
                        Data = data,
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
