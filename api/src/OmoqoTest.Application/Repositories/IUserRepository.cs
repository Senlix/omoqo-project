using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}