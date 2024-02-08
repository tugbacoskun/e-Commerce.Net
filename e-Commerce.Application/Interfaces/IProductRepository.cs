using e_Commerce.Application.Dtos;
using e_Commerce.Application.Features.Category.Commands;
using e_Commerce.Domain.Entities;
using e_Commerce.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Application.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
         Task<List<Product>> GetByCurrencyType (CurrencyTypeLookup currencyType);
    }
}
