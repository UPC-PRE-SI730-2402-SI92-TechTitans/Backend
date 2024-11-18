using Domain.FinanceGuard.Model.Entities;
using Domain.Groups.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Shared.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySQL(configuration["ConnectionStrings:financeGuardConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // FLuent API
        builder.Entity<Contact>().ToTable("Contact")
            .Property(c => c.Name).HasMaxLength(25).IsRequired();

        builder.Entity<Contact>().ToTable("Contact")
            .Property(c => c.Email).HasMaxLength(35).IsRequired();

            builder.Entity<Group>().HasKey(g => g.Id);
        builder.Entity<Participant>().HasKey(p => p.Id);

        builder.Entity<Group>()
            .HasMany(g => g.Participants)
            .WithOne(p => p.Group)
            .HasForeignKey(p => p.GroupId);
    }
}
