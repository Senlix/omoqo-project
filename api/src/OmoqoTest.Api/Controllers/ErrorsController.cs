using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace OmoqoTest.Api.Controllers
{
    [Route("error")]
    public class ErrorsController : ControllerBase
    {

        private ObjectResult HandleError()
        {
            Exception? ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            return Problem(detail: ex?.Message);
        }

        [HttpGet]
        public IActionResult ErrorGet() => HandleError();

        [HttpPost]
        public IActionResult ErrorPost() => HandleError();

        [HttpPut]
        public IActionResult ErrorPut() => HandleError();

        [HttpDelete]
        public IActionResult ErrorDelete() => HandleError();

    }
}