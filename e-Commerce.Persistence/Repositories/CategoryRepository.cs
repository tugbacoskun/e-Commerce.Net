using e_Commerce.Application.Interfaces;
using e_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence.Repositories
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(eCommerceDbContext dbContext) : base(dbContext)
        {

        }
    }
}
