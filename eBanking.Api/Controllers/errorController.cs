using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBanking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class errorController : ControllerBase
    {
        /// <summary>
        /// error details for development environment
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/error-dev")]
        public IActionResult ErrorLocalDevelopment(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
        }

        /// <summary>
        /// error details for non-dev environments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
