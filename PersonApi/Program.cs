using FluentValidation;
using FluentValidation.AspNetCore;
using PersonApi.Application.Commands;
using PersonApi.Application.MappingProfiles;
using PersonApi.Application.Validators;
using PersonApi.Database;
using PersonApi.Database.MySql;
using PersonApi.Database.Repositories;
using PersonApi.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors",
        policyBuilder => policyBuilder.WithOrigins(builder.Configuration.GetValue<string>("Origin"))
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PersonProfile).Assembly);
builder.Services.AddDbContext<MySqlDbContext>();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetPersonCommand).Assembly));

builder.Services.AddScoped<IPersonRepository, PersonRepository>(o =>
    new PersonRepository(o.GetRequiredService<MySqlDbContext>())
);

builder.Services.AddSingleton<IHostedService, MigrationService<MySqlDbContext>>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowCors");
app.UseAuthorization();
app.MapControllers();
app.Run();

// Created for integration tests
namespace PersonApi
{
    public partial class Program
    {
    }
}
