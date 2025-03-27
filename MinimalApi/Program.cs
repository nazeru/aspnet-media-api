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
using MinimalApi.Core.Validators;
using MinimalApi.Web;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlite("Data Source=./app.db");
    //.UseModel(ApplicationContextModel.Instance.CreateModel());
});
services.AddScoped<IDatabaseFactory, DatabaseFactory>();
services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

services.AddTransient<IEntityRepository<UserEntity>, BaseEntityRepository<UserEntity>>();
services.AddTransient<IEntityRepository<GenreEntity>, BaseEntityRepository<GenreEntity>>();
services.AddTransient<IEntityRepository<PlatformEntity>, BaseEntityRepository<PlatformEntity>>();
services.AddTransient<IEntityRepository<MediaEntity>, BaseEntityRepository<MediaEntity>>();
services.AddTransient<IEntityRepository<MovieEntity>, BaseEntityRepository<MovieEntity>>();
services.AddTransient<IEntityRepository<MusicEntity>, BaseEntityRepository<MusicEntity>>();

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

