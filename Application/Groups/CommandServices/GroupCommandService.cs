using System;
using System.Threading.Tasks;
using Domain.Groups.Model.Entities;
using Domain.Groups.Repositories;

namespace Application.Groups.CommandServices
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