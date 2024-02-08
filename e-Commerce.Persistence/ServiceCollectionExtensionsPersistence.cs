using e_Commerce.Application.Interfaces;
using e_Commerce.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Persistence
{
    public static class ServiceCollectionExtensionsPersistence
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        { 
           services.AddScoped<IProductRepository,ProductRepository>();

            return services;
        }
    }
}
