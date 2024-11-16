using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Expenses.Persistence;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Expenses.Persistence
{
    public class ExpenseDbContextFactory : IDesignTimeDbContextFactory<ExpenseDbContext>
    {
        public ExpenseDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExpenseDbContext>();

            // Configura la cadena de conexión
            var connectionString = "Server=localhost;Database=finance-guard-db;User=root;Password=root;Port=3306";

            optionsBuilder.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            return new ExpenseDbContext(optionsBuilder.Options);
        }
    }
}