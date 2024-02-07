using e_Commerce.Domain.Base;
using e_Commerce.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Domain.Entities
{
    public class ExchangeRate : BaseEntity
    {
        public CurrencyTypeLookup CurrencyTypeId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
    }
}
