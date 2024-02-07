using e_Commerce.Application;
using e_Commerce.Application.Features.ExchangeRate.Commands;
using e_Commerce.Application.Jobs;
using e_Commerce.Persistence;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<eCommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IeCommerceDbContext, eCommerceDbContext>();

ServiceCollectionExtensionsApplication.ServiceCollectionExtension(builder.Services,builder.Configuration);

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

 

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();
app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();
RecurringJobs.Start();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
