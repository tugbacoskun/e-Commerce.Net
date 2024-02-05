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

namespace e_Commerce.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequestHandler<UpdateProductCommandRequest, DataResult>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;

        public UpdateProductCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DataResult> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateProductFluentValidatior();
                var result = validator.Validate(request);
                if (result.IsValid)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);
                    product.ProductCurrency = request.ProductCurrency;
                    product.Price = request.Price;
                    product.CategoryId = request.CategoryId;
                    product.Description = request.Description;
                    product.Image = request.Image;
                    product.Name = request.Name;


                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    var data = _mapper.Map<UpdateProductCommandResponse>(product);

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
