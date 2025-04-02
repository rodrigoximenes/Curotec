using Curotec.Application.Mappers;
using Curotec.Application.Services;
using Curotec.Application.Services.Interfaces;
using Curotec.Data;
using Curotec.Data.Repository;
using Curotec.Data.Repository.Interfaces;
using Curotec.Domain.Validators;
using Curotec.WebAPI.Middlewares;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string dbServer = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"
    ? "sqlserver-container" 
    : "localhost,1433";     

builder.Configuration["ConnectionStrings:DefaultConnection"] =
    $"Server={dbServer};Database=master;User Id=sa;Password=@Password123!;TrustServerCertificate=True;";

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configurações padrão
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependências
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(DTOsToDomainMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<TodoValidator>();

var app = builder.Build();

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curotec API");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();
