using Domain.Expenses.Model.Entities;

namespace Domain.Expenses.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(Guid id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Guid id);
    }
}