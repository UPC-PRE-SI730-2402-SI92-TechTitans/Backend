namespace Domain.Contact.Model.Commands;

public record UpdateContactCommand(int Id, string Name, string Email);