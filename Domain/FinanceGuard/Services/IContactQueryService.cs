using Domain.FinanceGuard.Model.Entities;
using Domain.FinanceGuard.Model.Queries;

namespace Domain.FinanceGuard.Services;

public interface IContactQueryService
{
    Task<IEnumerable<Contact>?> Handle(GetAllContactsQuery query);
    Task<Contact?> Handle(GetContactByIdQuery query);
}