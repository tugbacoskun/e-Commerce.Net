using AutoMapper;
using e_Commerce.Application.Dtos;
using e_Commerce.Application.Fluent_Validation;
using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class AddProductCommand : IRequestHandler<AddProductCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public AddProductCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DataResult> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddProductFluentValidatior();
                var result = validator.Validate(request);

                if (result.IsValid)
                {
                    var product = _mapper.Map<Domain.Entities.Product>(request);
                    await _context.Products.AddAsync(product, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var data = _mapper.Map<AddProductCommandResponse>(product);

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
