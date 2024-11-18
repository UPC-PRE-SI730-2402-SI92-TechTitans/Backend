namespace Domain.Security.Model.Commands;

public record SignInCommand(string Email, string Password);