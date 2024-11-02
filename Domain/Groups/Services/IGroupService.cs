using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Groups.Model.Entities;

namespace Project.Domain.Groups.Services
{
    public interface IGroupService
    {
        Task<Group> CreateGroupAsync(Group group);
        Task<IEnumerable<Group>> GetGroupsByUserAsync(Guid userId);
    }
}