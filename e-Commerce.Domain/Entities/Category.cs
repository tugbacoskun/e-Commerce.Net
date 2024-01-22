using e_Commerce.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Domain.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
