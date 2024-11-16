using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Expenses.Model.Entities;
using Domain.Expenses.Repositories;
using Infrastructure.Expenses.Persistence;

namespace Infrastructure.Expenses.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext _context;

        public ExpenseRepository(ExpenseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .Include(e => e.Items)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var expense = await GetByIdAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }
}