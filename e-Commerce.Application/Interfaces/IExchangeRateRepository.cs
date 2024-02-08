using e_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Interfaces
{
    public interface IExchangeRateRepository : IRepository<ExchangeRate>
    {

        Task<ExchangeRate> FirstOrDefaultAsync(Expression<Func<ExchangeRate, bool>> predicate);

    }
}
