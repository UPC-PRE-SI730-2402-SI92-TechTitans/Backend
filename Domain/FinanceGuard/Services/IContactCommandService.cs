using Domain.FinanceGuard.Model.Commands;

namespace Domain.FinanceGuard.Services;

public interface IContactCommandService
{
    Task<int> Handle(CreateContactCommand command);
    Task<bool> Handle(UpdateContactCommand command);
    Task<bool> Handle(DeleteContactCommand command);
}