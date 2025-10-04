using Adset.Veiculos.Infraestrutura.Dados;
using Adset.Veiculos.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Adset.Veiculos.Api.Infraestrutura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IItemOpicionalRepository, ItemOpicionalRepository>();
builder.Services.AddScoped<IVeiculoFotoRepository, VeiculoFotoRepository>();
builder.Services.AddScoped<IVeiculoOpicionalRepository, VeiculoOpicionalRepository>();
builder.Services.AddScoped<IVeiculoPortalPacoteRepository, VeiculoPortalPacoteRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
