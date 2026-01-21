namespace ProductHub.Application.Auth.Login;

public class LoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> AuthenticateAsync(LoginCommand command)
    {
        var user = await _userRepository.GetByUsernameAsync(command.Username)
            ?? throw new InvalidOperationException("Invalid credentials.");

        if (!_passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid credentials.");

        return user;
    }
}
