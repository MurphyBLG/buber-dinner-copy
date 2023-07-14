namespace BuberDinner.Contracts.Authentication;
    
public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);