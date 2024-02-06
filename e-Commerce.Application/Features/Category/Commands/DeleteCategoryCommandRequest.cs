﻿using e_Commerce.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Category.Commands
{
    public class DeleteCategoryCommandRequest: IRequest<DataResult>
    {
        public Guid Id { get; set; }
    }
}
