namespace Domain.FinanceGuard.Model.Commands;

public record UpdateContactCommand(int Id, string Name, string Email);