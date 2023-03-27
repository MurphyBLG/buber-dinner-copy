namespace BuberDinner.Contracts.Authentication;

public record AuthenticationRresponse(
    Guid Id,
    string FirstName,
    string Lastname,
    string Email,
    string Token);
