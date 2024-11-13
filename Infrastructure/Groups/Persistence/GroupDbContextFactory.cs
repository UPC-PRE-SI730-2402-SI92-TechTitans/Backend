using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Groups.Persistence
{
    public class GroupDbContextFactory : IDesignTimeDbContextFactory<GroupDbContext>
    {
        public GroupDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Presentation"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("financeGuardConnection")
                                   ?? throw new InvalidOperationException("Connection string 'financeGuardConnection' not found.");

            var optionsBuilder = new DbContextOptionsBuilder<GroupDbContext>();
            optionsBuilder.UseMySQL(connectionString);

            return new GroupDbContext(optionsBuilder.Options);
        }
    }
}