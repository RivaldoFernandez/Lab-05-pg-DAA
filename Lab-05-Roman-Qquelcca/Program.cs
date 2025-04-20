using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.Repository;
using Lab_05_Roman_Qquelcca.Repository.Unit;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor con soporte JSON que ignora valores vacíos
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

// Conexión a la base de datos MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TecsupDB>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Inyección de dependencias
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = string.Empty;
});

// API Controllers
app.MapControllers();

app.Run();