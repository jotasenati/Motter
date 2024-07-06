using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Motter.Application.Commands.Motos;
using Motter.Application.Interfaces;
using Motter.Application.Validators;
using Motter.Domain.Interfaces;
using Motter.Infrastructure.Persistence.Contexts;
using Motter.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


//Dependencie Injection
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IPlacaUnicaValidator, PlacaUnicaValidator>();
builder.Services.AddScoped<IValidator<CreateMotoCommand>, MotoValidator>();




var app = builder.Build();

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
