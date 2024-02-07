using e_Commerce.Application.Features.ExchangeRate.Commands;
using e_Commerce.Application.Jobs;
using e_Commerce.Application.Redis;
using Hangfire;
using Hangfire.MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace e_Commerce.Application
{
    public static class ServiceCollectionExtensionsApplication
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
            
            });
            services.AddScoped<UpdateCommandExchangeRate>();

            return services;
        }
    }
}