namespace ProductHub.Application.Auth.Login;

public class LoginService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<User> AuthenticateAsync(LoginCommand command)
    {
        var user = await _userRepository.GetByUsernameAsync(command.Username)
            ?? throw new InvalidOperationException("Invalid credentials.");

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid credentials.");

        return user;
    }
}
