using ErrorOr;
using MediatR;
using OmoqoTest.Application.Authentication.Common;
using OmoqoTest.Application.Common.Interfaces.Authentication;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator iJwtTokenGenerator
        ) : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _iJwtTokenGenerator = iJwtTokenGenerator;

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Check if user exists
            if (await _userRepository.GetUserByEmailAsync(query.Email) is not User user || user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            string token = _iJwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}