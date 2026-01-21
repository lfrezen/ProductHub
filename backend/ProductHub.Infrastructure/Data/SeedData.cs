using ProductHub.Application.Auth;

namespace ProductHub.Infrastructure.Data;

public static class SeedData
{
    public static void Seed(ProductHubDbContext context)
    {
        if (context.Set<User>().Any())
            return;

        var user = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
        };

        context.Set<User>().Add(user);
        context.SaveChanges();
    }
}
