using e_Commerce.Application.Features.Product.Commands;
using e_Commerce.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Fluent_Validation
{
    public class AddProductFluentValidatior: AbstractValidator<AddProductCommandRequest>
    {
        public AddProductFluentValidatior()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Ürün adını giriniz.");
            RuleFor(product => product.Description).NotEmpty().WithMessage("Ürün açıklaması giriniz.");
            RuleFor(product => product.Price).NotEmpty().WithMessage("Ürün fiyatını giriniz.");
            RuleFor(product => product.ProductCurrency).NotEmpty().WithMessage("Ürün para birimini giriniz.");
            RuleFor(product => product.Image).NotEmpty().WithMessage("Ürün resmini giriniz.");
            
        }
    }
}
