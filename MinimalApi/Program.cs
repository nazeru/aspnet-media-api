using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using MinimalApi.Core;
using Swashbuckle.AspNetCore.Swagger;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;
using MinimalApi.Data;
using MinimalApi.Data.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MinimalApi.Core.Caching;
using MinimalApi.Core.Validators;
using MinimalApi.Web;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<ApplicationContext>((serviceProvider, options) =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var dbEngine = config.GetValue<string>("App:DatabaseEngine");

    if (dbEngine == "InMemory")
    {
        options.UseInMemoryDatabase("TestDb");
    }
    else
    {
        options.UseSqlite("Data Source=./app.db");
    }
});
services.AddScoped<IDatabaseFactory, DatabaseFactory>();
services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

services.AddTransient<ICacheManager, NoopCacheManager>();
services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));

services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }