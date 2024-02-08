using e_Commerce.Application.Interfaces;
using e_Commerce.Domain.Entities;
using e_Commerce.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence.Repositories
{
    public class ExchangeRateRepository: Repository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(eCommerceDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<ExchangeRate> FirstOrDefaultAsync(Expression<Func<ExchangeRate, bool>> predicate)
        {
           return await _dbContext.ExchangeRates.FirstOrDefaultAsync(x => x.CurrencyTypeId == CurrencyTypeLookup.USD);
            
        }
    }
}
