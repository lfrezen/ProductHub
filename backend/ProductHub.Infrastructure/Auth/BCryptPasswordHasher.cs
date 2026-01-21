using ProductHub.Application.Auth;

namespace ProductHub.Infrastructure.Auth;

public class BCryptPasswordHasher : IPasswordHasher
{
    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
