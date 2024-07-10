using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Motter.Application.Commands.Entregadores;
using Motter.Application.Commands.Locacoes;
using Motter.Application.Commands.Motos;
using Motter.Application.Handlers;
using Motter.Application.Handlers.EntregadorHandler;
using Motter.Application.Handlers.LocacaoHandler;
using Motter.Application.Handlers.MotoHandler;
using Motter.Application.Interfaces;
using Motter.Application.Mappings;
using Motter.Application.Queries.Entregadores;
using Motter.Application.Queries.Locacoes;
using Motter.Application.Queries.Motos;
using Motter.Application.Validators.EntregadorValidator;
using Motter.Application.Validators.LocacaoValidator;
using Motter.Application.Validators.MotoValidator;
using Motter.Controllers;
using Motter.Domain.Interfaces;
using Motter.Infrastructure.Messaging;
using Motter.Infrastructure.Persistence.Contexts;
using Motter.Infrastructure.Persistence.Repositories;
using Motter.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite().MigrationsAssembly("Motter.Infrastructure")); // Combinação dos lambdas
});

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Injeção de dependência
builder.Services.AddScoped<IMotoRepository, MotoRepository>();
builder.Services.AddScoped<IPlacaUnicaValidator, PlacaUnicaValidator>();
builder.Services.AddSingleton<IMessageProducer, RabbitMQMessageProducer>();
builder.Services.AddScoped<IValidator<CreateMotoCommand>, MotoValidator>();
builder.Services.AddScoped<IEntregadorRepository, EntregadorRepository>();
builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>(); // Adicionando o repositório de locação
builder.Services.AddScoped<IImagemStorageService, LocalDiskImageStorageService>();

// Registrando os validadores
builder.Services.AddScoped<IValidator<CreateEntregadorCommand>, CreateEntregadorCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateEntregadorCommand>, UpdateEntregadorCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateEntregadorImagemCommand>, UpdateEntregadorImagemCNHCommandValidator>();
builder.Services.AddScoped<IValidator<CreateLocacaoCommand>, CreateLocacaoCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateLocacaoCommand>, UpdateLocacaoCommandValidator>();

// Registrando os handlers e queries
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateMotoCommand).Assembly,
    typeof(GetAllMotos).Assembly,
    typeof(CreateMotoHandler).Assembly,
    typeof(CreateEntregadorCommand).Assembly,
    typeof(GetAllEntregadoresQueryHandler).Assembly,
    typeof(CreateEntregadorCommandHandler).Assembly,
    typeof(UpdateEntregadorImagemCommandHandler).Assembly,
    typeof(DeleteEntregadorCommandHandler).Assembly,
    typeof(CreateLocacaoCommand).Assembly,
    typeof(UpdateLocacaoCommand).Assembly,
    typeof(GetLocacaoByIdQuery).Assembly,
    typeof(GetAllLocacoesQuery).Assembly,
    typeof(CreateLocacaoCommandHandler).Assembly,
    typeof(UpdateLocacaoCommandHandler).Assembly,
    typeof(GetLocacaoByIdQueryHandler).Assembly,
    typeof(GetAllLocacoesQueryHandler).Assembly,
    typeof(DeleteMotoCommandHandler).Assembly
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
