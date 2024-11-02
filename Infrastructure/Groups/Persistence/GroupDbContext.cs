using Microsoft.EntityFrameworkCore;
using Project.Domain.Groups.Model.Entities;

namespace Project.Infrastructure.Groups.Persistence
{
    public class GroupDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public GroupDbContext(DbContextOptions<GroupDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Group>().HasKey(g => g.Id);
            modelBuilder.Entity<Participant>().HasKey(p => p.Id);
        }
    }
}