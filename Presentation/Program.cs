using Microsoft.EntityFrameworkCore;
using Infrastructure.Groups.Persistence;
using Infrastructure.Groups.Repositories;
using Domain.Groups.Repositories;
using Microsoft.OpenApi.Models;
using System;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("financeGuardConnection");

// Registro de GroupDbContext
builder.Services.AddDbContext<GroupDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
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

// Registro de repositorios y servicios para Groups
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<Application.Groups.CommandServices.GroupCommandService>();
builder.Services.AddScoped<Application.Groups.QueryServices.GroupQueryService>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configuración de JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Finance guard API", Version = "v1" });
});

var app = builder.Build();

// Asegurar la creación de la base de datos para ambos contextos
using (var scope = app.Services.CreateScope())
{
    var groupDbContext = scope.ServiceProvider.GetRequiredService<GroupDbContext>();

    groupDbContext.Database.EnsureCreated();
}

// Configuración de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Finance Guard API v1"));
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
