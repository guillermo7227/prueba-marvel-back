using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.API.Controllers;

public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error()
    {
        Exception? ex = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem(detail: ex?.Message);
    }
}