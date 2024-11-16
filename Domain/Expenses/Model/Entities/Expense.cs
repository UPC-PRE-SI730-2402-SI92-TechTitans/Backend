using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Expenses.Model.Entities
{
    public class Expense
    {
        public Guid Id { get; init; }
        public required string Description { get; init; } = string.Empty;
        public DateTime Date { get; init; }
        public decimal TotalAmount { get; init; }
        public Guid GroupId { get; set; }
        public List<ExpenseItem> Items { get; set; }

        public Expense()
        {
            Items = new List<ExpenseItem>();
            Date = DateTime.UtcNow;
        }
    }

    public class ExpenseItem
    {
        public Guid Id { get; init; }
        public required string Name { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; }
    }
}

