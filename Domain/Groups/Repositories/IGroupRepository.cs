﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Groups.Model.Entities;

namespace Domain.Groups.Repositories
{
    public interface IGroupRepository
    {
        Task<Group?> GetByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetAllAsync();
        Task AddAsync(Group group);
        Task UpdateAsync(Group group);
        Task DeleteAsync(Guid id);
    }
}
