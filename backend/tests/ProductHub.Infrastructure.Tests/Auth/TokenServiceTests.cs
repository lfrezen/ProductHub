using FluentAssertions;
using Microsoft.Extensions.Configuration;
using ProductHub.Api.Auth;
using ProductHub.Application.Auth;
using System.IdentityModel.Tokens.Jwt;

namespace ProductHub.Infrastructure.Tests.Auth;

public class TokenServiceTests
{
    private readonly IConfiguration _configuration;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        var configValues = new Dictionary<string, string>
        {
            {"Jwt:SecretKey", "test-secret-key-with-at-least-32-characters-for-testing"},
            {"Jwt:Issuer", "TestIssuer"},
            {"Jwt:Audience", "TestAudience"},
            {"Jwt:ExpirationInMinutes", "60"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configValues!)
            .Build();

        _tokenService = new TokenService(_configuration);
    }

    [Fact]
    public void Generate_WithValidUser_ShouldReturnToken()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "testuser" };

        // Act
        var token = _tokenService.Generate(user);

        // Assert
        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Generate_ShouldIncludeUsernameClaim()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "testuser" };

        // Act
        var token = _tokenService.Generate(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c =>
            c.Type == "unique_name" && c.Value == "testuser");
    }

    [Fact]
    public void Generate_ShouldIncludeSubClaim()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Username = "testuser" };

        // Act
        var token = _tokenService.Generate(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.Sub && c.Value == userId.ToString());
    }

    [Fact]
    public void Generate_ShouldHaveCorrectIssuerAndAudience()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "testuser" };

        // Act
        var token = _tokenService.Generate(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Issuer.Should().Be("TestIssuer");
        jwtToken.Audiences.Should().Contain("TestAudience");
    }

    [Fact]
    public void Generate_ShouldSetExpirationTime()
    {
        // Arrange
        var user = new User { Id = Guid.NewGuid(), Username = "testuser" };

        // Act
        var token = _tokenService.Generate(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.ValidTo.Should().BeCloseTo(
            DateTime.UtcNow.AddMinutes(60),
            TimeSpan.FromSeconds(10));
    }
}