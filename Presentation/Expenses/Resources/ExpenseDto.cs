namespace Presentation.Expenses.Resources
{
    public class ExpenseDto
    {
        public Guid? Id { get; set; }
        public required string Description { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TotalAmount { get; set; }
        public Guid? GroupId { get; set; }
        public List<ExpenseItemDto>? Items { get; set; } = new List<ExpenseItemDto>();
    }
}