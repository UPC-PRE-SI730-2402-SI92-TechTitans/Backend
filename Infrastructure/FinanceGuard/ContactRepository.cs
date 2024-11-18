using Domain.FinanceGuard.Model.Entities;
using Domain.FinanceGuard.Repositories;
using Infrastructure.Shared.Persistence.EFC.Configuration;
using Infrastructure.Shared.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.FinanceGuard;

public class ContactRepository(AppDbContext context) : BaseRepository<Contact>(context), IContactRepository
{
    public async Task<Contact?> FindByEmailAsync(string email)
    {
        return await context.Contacts.Where(c => c.Email == email).FirstOrDefaultAsync();
    }
}