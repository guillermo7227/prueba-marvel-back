using Marvel.Domain.Entities;

namespace Marvel.Application.Contracts;

public interface IUserRepository
{
    public Task<ApplicationUser> GetUserByEmailAsync(string email);
}