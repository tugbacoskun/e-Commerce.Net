using e_Commerce.Application.Features.Category.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Fluent_Validation
{
    public class UpdateCategoryFluentValidator: AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryFluentValidator() 
        {
            RuleFor(category=>category.Name).NotEmpty().WithMessage("Güncellemek istediğiniz kategori adını giriniz.");
        }

    }
}
