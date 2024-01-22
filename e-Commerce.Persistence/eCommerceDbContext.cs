using e_Commerce.Application.Interfaces;
using e_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence
{
    public class eCommerceDbContext: DbContext, IeCommerceDbContext

    {
    public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options) : base(options)
        {

        }
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
