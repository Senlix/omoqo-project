using ErrorOr;
using MediatR;
using OmoqoTest.Application.Authentication.Common;

namespace OmoqoTest.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}