using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Groups.Model.Entities;

namespace Domain.Groups.Services
{
    public interface IGroupService
    {
        Task<Group> CreateGroupAsync(Group group);
        Task<IEnumerable<Group>> GetGroupsByUserAsync(Guid userId);
    }
}