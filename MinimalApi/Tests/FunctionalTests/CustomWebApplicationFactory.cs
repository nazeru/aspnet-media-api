using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Data;
using System.Linq;
using MinimalApi.Core.Entities;

namespace MinimalApi.Tests.FunctionalTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configBuilder =>
        {
            var defaultConfig = new Dictionary<string, string>()
            {
                {"ProcessManagement:NodeNameStrategy", "Guid"},
                {"Messaging:Transport", "InMemory"},
                {"App:DatabaseEngine", "InMemory"},
                {"Application:IsSut", "true"},
            };
            configBuilder.AddInMemoryCollection(defaultConfig);
        });

        builder.ConfigureServices(services =>
        {
            // Удаляем оригинальную регистрацию ApplicationContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Добавляем InMemory базу для тестов
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });
        });
    }
}
