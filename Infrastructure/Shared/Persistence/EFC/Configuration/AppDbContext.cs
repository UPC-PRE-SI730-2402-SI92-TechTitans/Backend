using Domain.FinanceGuard.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Shared.Persistence.EFC.Configuration;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySQL(_configuration["ConnectionStrings:financeGuardConnection"]);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // FLuent API
        builder.Entity<Contact>().ToTable("Contact")
            .Property(c => c.Name).HasMaxLength(25).IsRequired();

        builder.Entity<Contact>().ToTable("Contact")
            .Property(c => c.Email).HasMaxLength(35).IsRequired();
    }
}