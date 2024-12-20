using Microsoft.EntityFrameworkCore;
using Domain.Groups.Model.Entities;
using Domain.Groups.Repositories;
using Infrastructure.Shared.Persistence.EFC.Configuration;

namespace Infrastructure.Groups.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Group?> GetByIdAsync(Guid id) =>
            await _context.Groups
                .Include(g => g.Participants)
                .FirstOrDefaultAsync(g => g.Id == id);

        public async Task<IEnumerable<Group>> GetAllAsync() =>
            await _context.Groups
                .Include(g => g.Participants)
                .ToListAsync();

        public async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }
    }
}
