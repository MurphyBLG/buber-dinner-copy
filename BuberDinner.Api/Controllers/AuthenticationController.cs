using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;

namespace BuberDinner.Api.Controllers;

[AllowAnonymous]
[Route("/api/[controller]")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register-user")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest registerUserRequest)
    {
        var command = _mapper.Map<RegisterUserCommand>(registerUserRequest);
        
        var authenticationResult = await _mediator.Send(command);

        return authenticationResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var query = _mapper.Map<LoginQuery>(loginRequest);
        var authenticationResult = await _mediator.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        
        return authenticationResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }
}
