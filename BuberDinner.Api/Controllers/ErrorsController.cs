using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class ErrorsController : Controller
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}