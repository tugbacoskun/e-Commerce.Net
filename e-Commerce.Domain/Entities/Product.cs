using e_Commerce.Domain.Base;
using e_Commerce.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Domain.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public CurrencyTypeLookup ProductCurrency { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
