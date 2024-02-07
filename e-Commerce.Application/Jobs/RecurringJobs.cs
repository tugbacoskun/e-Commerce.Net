using e_Commerce.Application.Features.ExchangeRate.Commands;
using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Jobs
{
    public static class RecurringJobs
    {
        public static void Start()
        {
            RecurringJob.AddOrUpdate<ExchangeRateUpdaterJob>(nameof(ExchangeRateUpdaterJob),
      job => job.UpdateCommandExchangeRate(), "*/5 * * * * *");

        }
    }


    public class ExchangeRateUpdaterJob
    {
        private readonly IMediator _mediator;

        public ExchangeRateUpdaterJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task UpdateCommandExchangeRate()
        {
            await _mediator.Send(new UpdateCommandRequestExchangeRate());
        }
    }


}
