namespace BuberDinner.Contracts.Authentication;
    
public record RegisterUserRequest(
    string FirstName,
    string Lastname,
    string Email,
    string Password);