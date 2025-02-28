using Marvel.Application.Contracts;
using Marvel.Domain.Entities;
using Marvel.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Marvel.Infraestructure.Implementation;

public class UserRepository : IUserRepository
{
    private MarvelDbContext _context;

    public UserRepository(MarvelDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

        return user!;
    }

}