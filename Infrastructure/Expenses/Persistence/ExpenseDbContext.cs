using Domain.Expenses.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Expenses.Persistence
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseItem> ExpenseItems { get; set; }

        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la entidad Expense
            modelBuilder.Entity<Expense>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Expense>()
                .HasMany(e => e.Items)
                .WithOne(i => i.Expense)
                .HasForeignKey(i => i.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración para la entidad ExpenseItem
            modelBuilder.Entity<ExpenseItem>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<ExpenseItem>()
                .Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ExpenseItem>()
                .Property(i => i.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}