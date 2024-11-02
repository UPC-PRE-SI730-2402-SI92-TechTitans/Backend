namespace Project.Domain.Groups.Model.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Dictionary<Guid, Participant> Participants { get; set; }

        public Group()
        {
            Participants = new Dictionary<Guid, Participant>();
            CreationDate = DateTime.UtcNow;
        }
    }

    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal PendingPayment { get; set; }
        public DateTime Date { get; set; }
    }
}