using BuberDinner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controllers;

[Authorize]
[ApiController]
public class ApiController : Controller
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 0)
            return Problem();

        if (errors.All(error => error.Type == ErrorType.Validation))
            return HandleValidationProblem(errors);

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return HandleSingleProblem(errors[0]);
    }

    private IActionResult HandleSingleProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult HandleValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        errors.ForEach(error => 
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description));

        return ValidationProblem(modelStateDictionary);
    }
}