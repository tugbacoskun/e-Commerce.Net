using e_Commerce.Application.Interfaces;
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
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public GetListExchangeRate(IMediator mediator, IExchangeRateRepository exchangeRateRepository)
        {
            _mediator = mediator;
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<DataResult>Handle(GetExchangeRateQueryRequest request, CancellationToken cancellationToken)
        {
            var exchangeRateList = await _exchangeRateRepository.GetAllAsync();
            return new DataResult
            {
                Data = exchangeRateList,
                IsSuccess = true,
                Message = string.Empty
            };
        }
    }

}
