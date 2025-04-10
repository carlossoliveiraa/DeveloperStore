using DeveloperStore.Sales.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperStore.Sales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Success(string? message = null)
            => Ok(ApiResponse.Ok(message));

        protected IActionResult Success<T>(T data, string? message = null)
            => Ok(ApiResponse<T>.Ok(data, message));

        protected IActionResult Failure(string message, int statusCode = 400)
            => StatusCode(statusCode, ApiResponse.Fail(message));
    }
}
