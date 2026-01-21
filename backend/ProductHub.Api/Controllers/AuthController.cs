using Microsoft.AspNetCore.Mvc;
using ProductHub.Api.Auth;
using ProductHub.Application.Auth.Login;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginService _loginService;
    private readonly TokenService _tokenService;

    public AuthController(
        LoginService loginService,
        TokenService tokenService)
    {
        _loginService = loginService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var user = await _loginService.AuthenticateAsync(command);
        var token = _tokenService.Generate(user);

        return Ok(new { token });
    }
}
