namespace Presentation.Groups.Resources
{
    public class ParticipantDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PendingPayment { get; set; }
        public DateTime? Date { get; set; }
        public Guid? GroupId { get; set; }
    }
}