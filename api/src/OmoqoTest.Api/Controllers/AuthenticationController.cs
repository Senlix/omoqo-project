using Microsoft.AspNetCore.Mvc;
using OmoqoTest.Contracts.Authentication;
using MediatR;
using OmoqoTest.Application.Authentication.Commands.Register;
using OmoqoTest.Application.Authentication.Common;
using OmoqoTest.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using ErrorOr;

namespace OmoqoTest.Api.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController(ISender mediator, IMapper mapper) : MediatRController(mediator, mapper)
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            RegisterCommand command = _mapper.Map<RegisterCommand>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                Problem
            );
        }      


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginQuery query = _mapper.Map<LoginQuery>(request);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

            return authResult.Match(
               authResult => Ok(_mapper.Map<AuthenticationResult>(authResult)),
               Problem
           );
        }
    }
}