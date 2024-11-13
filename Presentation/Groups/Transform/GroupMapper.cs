using Domain.Groups.Model.Entities;
using Presentation.Groups.Resources;

namespace Presentation.Groups.Transform
{
    public static class GroupMapper
    {
        public static GroupDto ToResource(this Group group)
        {
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                CreationDate = group.CreationDate,
                Participants = (group.Participants ?? new List<Participant>())
                    .Select(p => new ParticipantDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Amount = p.Amount,
                        PendingPayment = p.PendingPayment,
                        Date = p.Date
                    }).ToList()
            };
        }

        public static Group ToDomainModel(this GroupDto dto)
        {
            return new Group
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreationDate = DateTime.UtcNow,
                Participants = (dto.Participants ?? new List<ParticipantDto>())
                    .Select(p => new Participant
                    {
                        Id = Guid.NewGuid(),
                        Name = p.Name,
                        Amount = p.Amount,
                        PendingPayment = p.PendingPayment,
                        Date = p.Date
                    }).ToList()
            };
        }
    }
}