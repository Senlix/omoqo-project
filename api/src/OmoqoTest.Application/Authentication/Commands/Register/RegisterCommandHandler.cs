using ErrorOr;
using MediatR;
using OmoqoTest.Application.Authentication.Common;
using OmoqoTest.Application.Common.Interfaces.Authentication;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandle(
            IUserRepository userRepository,
            IJwtTokenGenerator iJwtTokenGenerator
        ) : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _iJwtTokenGenerator = iJwtTokenGenerator;
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Check if user exists
            if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
            {
                return Errors.User.DuplicatedEmail;
            }

            // Create user
            User user = new()
            {
                Name = command.Name,
                Email = command.Email,
                Password = command.Password
            };

            await _userRepository.AddAsync(user);

            // Create JWT Token
            string token = _iJwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}