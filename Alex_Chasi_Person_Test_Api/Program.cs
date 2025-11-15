using Alex_Chasi_Person_Test_Api.Data;
using Alex_Chasi_Person_Test_Api.Repositories;
using Alex_Chasi_Person_Test_Api.Repositories.Interfaces;
using Alex_Chasi_Person_Test_Api.Services;
using Alex_Chasi_Person_Test_Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//AGREGO INICIO
// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Registrar repositorios
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Registrar servicios
builder.Services.AddScoped<IPersonService, PersonService>();

// Configurar HttpClient para el servicio externo
builder.Services.AddHttpClient<IExternalUserService, ExternalUserService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
//AGREGO FIN

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Person Management API",
        Version = "v1",
        Description = "API para la gestión de personas con integración de servicios externos"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
