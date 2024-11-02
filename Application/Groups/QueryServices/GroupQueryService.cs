using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Groups.Model.Entities;
using Project.Domain.Groups.Repositories;

namespace Project.Application.Groups.QueryServices
{
    public class GroupQueryService
    {
        private readonly IGroupRepository _repository;

        public GroupQueryService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync() => await _repository.GetAllAsync();

        public async Task<Group> GetGroupByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
    }
}