using Microsoft.EntityFrameworkCore;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Infrastructure.Persistence.Repositories
{
    public class UserRepository(OmoqoTestDbContext dbContext) : Repository<User>(dbContext), IUserRepository
    {
        private readonly OmoqoTestDbContext _dbContext = dbContext;

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}