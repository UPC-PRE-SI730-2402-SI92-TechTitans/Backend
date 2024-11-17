using Domain.Contact.Model.Entities;
using Domain.Contact.Model.Queries;
using Domain.Contact.Repositories;
using Domain.Contact.Services;

namespace Application.Contact.QueryServices;

public class ContactQueryService : IContactQueryService
{
    private readonly IContactRepository _repository;

    public ContactQueryService(IContactRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Contacto>?> Handle(GetAllContactsQuery query)
    {
        return await _repository.ListAsync();
    }

    public async Task<Contact?> Handle(GetContactByIdQuery query)
    {
        return await _repository.FindByIdAsync(query.Id);
    }
}
