using Domain.Contact.Model.Entities;
using Domain.Shared;

namespace Domain.Contact.Repositories;

public interface IContactRepository : IBaseRepository<Contacto>
{
    public Task<Contacto?> FindEmailAsync(string email);
}