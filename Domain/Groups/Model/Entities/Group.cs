namespace Domain.Groups.Model.Entities
{
    public class Group
    {
        public Guid? Id { get; init; }
        public required string Name { get; init; } = string.Empty;
        public required string Description { get; init; } = string.Empty;
        public DateTime? CreationDate { get; init; }
        public List<Participant>? Participants { get; set; }

        public Group()
        {
            Participants = new List<Participant>();
            CreationDate = DateTime.UtcNow;
        }
    }

    public class Participant
    {
        public Guid? Id { get; init; }
        public required string Name { get; init; } = string.Empty;
        public decimal? Amount { get; init; }
        public decimal? PendingPayment { get; init; }
        public DateTime? Date { get; init; }

        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}