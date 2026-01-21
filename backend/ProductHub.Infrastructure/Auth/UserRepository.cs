using Microsoft.EntityFrameworkCore;
using ProductHub.Application.Auth;
using ProductHub.Infrastructure.Data;

namespace ProductHub.Infrastructure.Auth;

public class UserRepository : IUserRepository
{
    private readonly ProductHubDbContext _context;

    public UserRepository(ProductHubDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}
