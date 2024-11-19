using Domain.FinanceGuard.Model.Entities;
using Domain.FinanceGuard.Model.Queries;
using Domain.FinanceGuard.Repositories;
using Domain.FinanceGuard.Services;

namespace Application.FinanceGuard.QueryServices;

public class ContactQueryService : IContactQueryService
{
    private readonly IContactRepository _repository;

    public ContactQueryService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Contact>?> Handle(GetAllContactsQuery query)
    {
        return await _repository.ListAsync();
    }

    public async Task<Contact?> Handle(GetContactByIdQuery query)
    {
        return await _repository.FindByIdAsync(query.Id);
    }
}