namespace ProductHub.Application.Auth;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}
