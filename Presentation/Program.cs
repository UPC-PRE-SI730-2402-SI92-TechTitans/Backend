using Application.FinanceGuard.CommandServices;
using Application.FinanceGuard.QueryServices;
using Domain.FinanceGuard.Repositories;
using Domain.FinanceGuard.Services;
using Domain.Groups.Repositories;
using Domain.Shared;
using Infrastructure.FinanceGuard;
using Infrastructure.Groups.Repositories;
using Infrastructure.Shared.Persistence.EFC.Configuration;
using Infrastructure.Shared.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactQueryService, ContactQueryService>();
builder.Services.AddScoped<IContactCommandService, ContactCommandService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<Application.Groups.CommandServices.GroupCommandService>();
builder.Services.AddScoped<Application.Groups.QueryServices.GroupQueryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Conexión a MySql
var connectionString = builder.Configuration.GetConnectionString("financeGuardConnection");

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

var app = builder.Build();

EnsureDataBaseCreation(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

void EnsureDataBaseCreation(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
}
