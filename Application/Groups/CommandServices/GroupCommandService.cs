using System;
using System.Threading.Tasks;
using Project.Domain.Groups.Model.Entities;
using Project.Domain.Groups.Repositories;

namespace Project.Application.Groups.CommandServices
{
    public class GroupCommandService
    {
        private readonly IGroupRepository _repository;

        public GroupCommandService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            await _repository.AddAsync(group);
            return group;
        }

        public async Task UpdateGroupAsync(Group group) => await _repository.UpdateAsync(group);

        public async Task DeleteGroupAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}