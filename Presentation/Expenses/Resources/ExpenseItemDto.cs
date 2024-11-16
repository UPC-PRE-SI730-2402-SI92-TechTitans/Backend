namespace Presentation.Expenses.Resources
{
    public class ExpenseItemDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public decimal? Amount { get; set; }
        public Guid? ExpenseId { get; set; }
    }
}