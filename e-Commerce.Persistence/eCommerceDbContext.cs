using Api.Identity;
using e_Commerce.Application.Interfaces;
using e_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence
{
    public class eCommerceDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IeCommerceDbContext

    {
        public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
    }
}
