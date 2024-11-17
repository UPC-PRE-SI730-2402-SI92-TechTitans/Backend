using Domain.FinanceGuard.Model.Entities;
using Domain.Shared;

namespace Domain.FinanceGuard.Repositories;

public interface IContactRepository : IBaseRepository<Contact>
{
    public Task<Contact?> FindByEmailAsync(string email);
}