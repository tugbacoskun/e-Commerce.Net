using e_Commerce.Application.Interfaces;
using e_Commerce.Domain.Entities;
using e_Commerce.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(eCommerceDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Product>> GetByCurrencyType(CurrencyTypeLookup currencyType)
        {
            return await _dbContext.Products.Where(x => x.ProductCurrency == currencyType).ToListAsync();
        }
    }
}
