using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register-user")]
    public IActionResult RegisterUser(RegisterUserRequest registerUserRequest)
    {
        var authResult = _authenticationService.RegisterUser(
            registerUserRequest.FirstName,
            registerUserRequest.Lastname,
            registerUserRequest.Email,
            registerUserRequest.Password);
        
        AuthenticationResponse authResponse = new(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        
        return Ok(authResponse);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var authResult = _authenticationService.Login(
            loginRequest.Email,
            loginRequest.Password);
        
        AuthenticationResponse authResponse = new(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
        
        return Ok(authResponse);
    }
}
