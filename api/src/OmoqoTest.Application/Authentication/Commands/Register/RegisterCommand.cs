using ErrorOr;
using MediatR;
using OmoqoTest.Application.Authentication.Common;

namespace OmoqoTest.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string Name, string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}