using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Common.Interfaces.Authentication{
    public interface IJwtTokenGenerator{
        string GenerateToken(User user);
    }
}