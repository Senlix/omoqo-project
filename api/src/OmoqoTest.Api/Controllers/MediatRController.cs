using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OmoqoTest.Api.Controllers
{
    [ApiController]
    // [Authorize]
    public class MediatRController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        protected async Task<ActionResult<TMapping>> ExecuteAsync<TCommandQuery, TRequest, TResult, TMapping>(
            TRequest request,
            bool ignoreResult = false)
        {
            if (request == null)
            {
                return Problem("Request cannot be null.");
            }

            TCommandQuery commandQuery = _mapper.Map<TCommandQuery>(request);

            if (commandQuery == null)
            {
                return Problem("CommandQuery cannot be null.");
            }

            var result = await _mediator.Send(commandQuery);

            if (result is ErrorOr<TResult> errorOrResult)
            {
                return errorOrResult.Match(
                    result =>
                    {
                        if (result != null)
                        {
                            return ignoreResult ? Ok() : Ok(_mapper.Map<TMapping>(result));
                        }
                        else
                        {
                            return Problem("Result is null.");
                        }
                    },
                    Problem
                );
            }
            else
            {
                return Problem();
            }
        }

        protected async Task<ActionResult<TResult>> ExecuteAsync<TCommandQuery, TRequest, TResult>(
            TRequest request,
            bool ignoreResult = false)
        {
            return await ExecuteAsync<TCommandQuery, TRequest, TResult, TResult>(request, ignoreResult);
        }
    }
}