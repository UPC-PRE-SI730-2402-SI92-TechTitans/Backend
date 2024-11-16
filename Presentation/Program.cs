using Microsoft.EntityFrameworkCore;
using Infrastructure.Groups.Persistence;
using Infrastructure.Groups.Repositories;
using Infrastructure.Expenses.Persistence;
using Domain.Groups.Repositories;
using Microsoft.OpenApi.Models;
using System;
using Domain.Expenses.Repositories;
using Infrastructure.Expenses.Repositories;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("financeGuardConnection");

builder.Services.AddDbContext<GroupDbContext>(options =>
{
    if (connectionString != null)
    {
        if (builder.Environment.IsDevelopment())
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        else if (builder.Environment.IsProduction())
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        }
    }
});

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<Application.Groups.CommandServices.GroupCommandService>();
builder.Services.AddScoped<Application.Groups.QueryServices.GroupQueryService>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Finance guard API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GroupDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1"));
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();