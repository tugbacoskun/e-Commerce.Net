using Api.Identity;
using e_Commerce.Application;
using e_Commerce.Application.Features.ExchangeRate.Commands;
using e_Commerce.Application.Interfaces.IdentityInterfaces;
using e_Commerce.Application.Jobs;
using e_Commerce.Persistence;
using Hangfire;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ServiceStack;
using ServiceStack.Text;
using StackExchange.Redis;
using System.Text;

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


ServiceCollectionExtensionsApplication.ServiceCollectionExtension(builder.Services, builder.Configuration);
ServiceCollectionExtensionsPersistence.ServiceCollectionExtension(builder.Services, builder.Configuration);
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));


builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<eCommerceDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("user");
            host.Password("password"); 
        });
    });
});



builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ClockSkew = TimeSpan.Zero,
    };
});
builder.Services.AddTransient<IClaimsService, ClaimsService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHangfireDashboard("/hangfire");
app.UseHangfireServer();
RecurringJobs.Start();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

 


app.UseSerilogRequestLogging();

app.UseStaticFiles();




app.Run();
