﻿using e_Commerce.Application.Dtos;
using e_Commerce.Application.Response;
using e_Commerce.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.Product.Commands
{
    public class UpdateProductCommandRequest : IRequest<DataResult>
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public CurrencyTypeLookup ProductCurrency { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime? UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
