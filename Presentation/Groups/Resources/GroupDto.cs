namespace Presentation.Groups.Resources
{
    public class GroupDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<ParticipantDto>? Participants { get; set; } = new List<ParticipantDto>();
    }
}