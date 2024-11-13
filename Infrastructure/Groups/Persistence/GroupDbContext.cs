using Microsoft.EntityFrameworkCore;
using Domain.Groups.Model.Entities;

namespace Infrastructure.Groups.Persistence
{
    public class GroupDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public GroupDbContext(DbContextOptions<GroupDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>().HasKey(g => g.Id);
            modelBuilder.Entity<Participant>().HasKey(p => p.Id);

            modelBuilder.Entity<Group>()
                .HasMany(g => g.Participants)
                .WithOne(p => p.Group)
                .HasForeignKey(p => p.GroupId);
        }
    }
}