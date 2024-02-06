using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using e_Commerce.Application.Hangfire;
using e_Commerce.Application.Redis;

namespace e_Commerce.Application
{
    public static class ServiceCollectionExtensionsApplication
    {
        public static IServiceCollection ServiceCollectionExtension(this IServiceCollection services, params Type[] types)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHangfire(config => config.UseMemoryStorage());
            services.AddTransient<HangfireService>();
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            return services;
        }
    }
}