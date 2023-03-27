using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : Controller
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        return Ok(registerRequest);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        return Ok(loginRequest);
    }
}
