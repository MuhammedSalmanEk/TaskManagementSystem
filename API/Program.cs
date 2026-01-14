using Microsoft.Extensions.DependencyInjection;
using Infrastructure;
using Application;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("TaskDb"));
builder.Services.AddScoped<ITaskService, TaskServices>();



builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("UserHeader", new OpenApiSecurityScheme
    {
        Name = "user",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "Simulated login (admin or user)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UserHeader"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
