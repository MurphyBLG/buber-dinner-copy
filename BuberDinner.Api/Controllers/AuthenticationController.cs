using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;

namespace BuberDinner.Api.Controllers;

[Route("/api/[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest registerUserRequest)
    {
        var command = new RegisterCommand(
            registerUserRequest.FirstName, 
            registerUserRequest.Lastname, 
            registerUserRequest.Email, 
            registerUserRequest.Password);
        
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
        var authenticationResult = await _mediator.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        
        return authenticationResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
    
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
    }
}
