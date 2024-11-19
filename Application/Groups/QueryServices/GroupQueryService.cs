using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Groups.Model.Entities;
using Domain.Groups.Repositories;

namespace Application.Groups.QueryServices
{
    public class GroupQueryService
    {
        private readonly IGroupRepository _repository;

        public GroupQueryService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync() => await _repository.GetAllAsync();

        public async Task<Group> GetGroupByIdAsync(Guid id)
        {
            var group = await _repository.GetByIdAsync(id);
            if (group == null)
            {
                throw new KeyNotFoundException($"Group with ID '{id}' was not found.");
            }
            return group;
        }
    }
}
