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

if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    builder.Configuration.AddJsonFile("appsettings.Docker.json", optional: true);
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


ConfigureDatabase(builder);
ConfigureServices(builder.Services);
ConfigureSwagger(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureDatabase(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<TodoDbContext>(options =>
        options.UseSqlServer(connectionString));
}

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    services.AddScoped<ITodoService, TodoService>();
    services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(DTOsToDomainMappingProfile));
    services.AddValidatorsFromAssemblyContaining<TodoValidator>();
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curotec API");
        });
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.MapControllers();
}
