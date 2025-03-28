using System.Text.Json;
using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using MinimalApi.Core.Entities;
using MinimalApi.Core.Repositories;
using Xunit;
using Xunit.Abstractions;

namespace MinimalApi.Tests.FunctionalTests;

[Collection("Controllers")]
public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    /// <summary>
    /// Фабрика создающее новый пакет системы под тестом
    /// </summary>
    private readonly CustomWebApplicationFactory<Program> _factory;
 
    /// <summary>
    /// Предоставляет возможность вывода дополнительной информации вместе с результатами теста
    /// </summary>
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Fixture _fixture;
 
    public UsersControllerTests (CustomWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _fixture = new Fixture();
        _fixture.Customizations.Add(
            new TypeRelay(typeof(MediaEntity), typeof(MovieEntity))
        );
        _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        _fixture.Customizations.Add(
            new TypeRelay(typeof(MediaEntity), typeof(MovieEntity))
        );
        
    }
 
    [Fact]
    public async Task GetUsers_ReturnsExactUser()
    {
        // Arrange
        var client = _factory.CreateClient();

        var user = _fixture.Build<UserEntity>()
            .Without(u => u.Medias)
            .Create();

        using var scope = _factory.Services.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IEntityRepository<UserEntity>>();
        await repo.InsertAsync(user);
        await repo.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/users");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var users = JsonSerializer.Deserialize<List<UserModel>>(content, options);
        var returnedUser = Assert.Single(users); // проверим, что ровно один
        
        _testOutputHelper.WriteLine(user.ToString());
        _testOutputHelper.WriteLine(returnedUser.ToString());

        returnedUser.Should().BeEquivalentTo(user, options => options
            .ExcludingMissingMembers());
    }


}