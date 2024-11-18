namespace Domain.Security.Model.Commands;

public record SignUpCommand(string Email, string Password, string Name, string Lastname);