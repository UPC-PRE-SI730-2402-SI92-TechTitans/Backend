using Domain.Contact.Model.Commands;

namespace Domain.Contact.Services;

public interface IContactCommandService
{
    Task<int> Handle(CreateContactCommand command);
    Task<bool> Handle(UpdateContactCommand command);
    Task<bool> Handle(DeleteContactCommand command);
}