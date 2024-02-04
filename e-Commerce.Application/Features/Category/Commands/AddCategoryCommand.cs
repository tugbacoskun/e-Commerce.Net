﻿using AutoMapper;
using e_Commerce.Application.Dtos;
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
    public class AddCategoryCommand : IRequestHandler<AddCategoryCommandRequest, AddCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IeCommerceDbContext _context;
        public AddCategoryCommand(IMapper mapper, IeCommerceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Entities.Category>(request);
            await _context.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddCategoryCommandResponse>(category);

        }
    }
}