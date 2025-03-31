using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Core;
using MinimalApi.Core.Repositories;
using MinimalApi.Data;
using MinimalApi.Data.Repositories;
using Newtonsoft.Json.Serialization;
using FluentValidation;
using MinimalApi.Connector;
using MinimalApi.Core.Caching;
using MinimalApi.Core.Validators;
using MinimalApi.Web;
using Refit;

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
services.AddRefitClient<IOMDbApiRestClient>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://www.omdbapi.com/");
    });

var app = builder.Build();

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

public partial class Program { }