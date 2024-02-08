using e_Commerce.Application.Response;
using e_Commerce.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Features.ExchangeRate.Queries
{
    public class GetListExchangeRate : IRequestHandler<GetExchangeRateQueryRequest, DataResult>
    {
        private readonly IMediator _mediator;
        private readonly IeCommerceDbContext _context;

        public GetListExchangeRate(IMediator mediator, IeCommerceDbContext context)
        {
            _mediator = mediator;
            _context = context;
        }

       public async Task<DataResult>Handle(GetExchangeRateQueryRequest request, CancellationToken cancellationToken)
        {
            var exchangeRateList = await _context.ExchangeRates.ToListAsync();
            return new DataResult
            {
                Data = exchangeRateList,
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }

}
