using Domain.Contact.Model.Entities;
using Domain.Contact.Model.Queries;

namespace Domain.Contact.Services;

public interface IContactQueryService
{
    Task<IEnumerable<Contacto>> Handle(GetAllContactsQuery query);
    Task<Contacto?> Handle(GetContactByIdQuery query);
}