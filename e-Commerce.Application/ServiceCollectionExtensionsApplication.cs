using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace e_Commerce.Application
{
    public static class ServiceCollectionExtensionsApplication
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, params Type[] types)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}