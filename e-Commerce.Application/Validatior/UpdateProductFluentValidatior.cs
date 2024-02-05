using e_Commerce.Application.Features.Product.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Fluent_Validation
{
    public class UpdateProductFluentValidatior: AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductFluentValidatior()
        {
            RuleFor(product=>product.Name).NotEmpty().WithMessage("Güncellemek istediğiniz ürün adını giriniz.");
            RuleFor(product => product.Description).NotEmpty().WithMessage("Güncellemek istediğiniz ürün açıklamasını giriniz.");
            RuleFor(product => product.Price).NotEmpty().WithMessage("Güncellemek istediğiniz ürün fiyatını giriniz.");
            RuleFor(product => product.ProductCurrency).NotEmpty().WithMessage("Güncellemek istediğiniz para birimini giriniz.");
            RuleFor(product => product.Image).NotEmpty().WithMessage("Güncellemek istediğiniz ürün resmini giriniz.");
        }
    }
}
