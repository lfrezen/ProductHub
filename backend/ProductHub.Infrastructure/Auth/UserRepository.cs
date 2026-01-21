using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Auth;
using ProductHub.Infrastructure.Data;

namespace ProductHub.Infrastructure.Auth;

public class UserRepository(ProductHubDbContext context) : IUserRepository
{
    private readonly ProductHubDbContext _context = context;

    public Task<User?> GetByUsernameAsync(string username)
    {
        return _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}
