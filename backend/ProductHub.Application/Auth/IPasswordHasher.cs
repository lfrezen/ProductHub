namespace ProductHub.Application.Auth;

public interface IPasswordHasher
{
    bool Verify(string password, string passwordHash);
}
