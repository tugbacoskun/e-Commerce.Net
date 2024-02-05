using e_Commerce.Application.Dtos;
using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Fluent_Validation
{
    public class AddCategoryFluentValidatior: AbstractValidator<AddCategoryCommandRequest>
    {
        public AddCategoryFluentValidatior() 
        {
            RuleFor(category => category.Name).NotEmpty().WithMessage("Kategori adı boş olamaz.");
        }

    }
}
