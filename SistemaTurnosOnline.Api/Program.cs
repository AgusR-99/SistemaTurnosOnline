using FluentValidation;
using SistemaTurnosOnline.Api.Data;
using SistemaTurnosOnline.Api.Repositories;
using SistemaTurnosOnline.Api.Repositories.Contracts;
using SistemaTurnosOnline.Api.Validator;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Validators;
using SistemaTurnosOnline.Shared.Validators.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProfesorRepository, ProfesorRepository>();
builder.Services.AddScoped<ICarreraRepository, CarreraRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();


builder.Services.AddScoped<IValidator<ProfesorForm>, ProfesorValidator>();
builder.Services.AddScoped<IValidateProfesor, ValidateProfesor>();

builder.Services.AddScoped<IValidator<Carrera>, CarreraValidator>();
builder.Services.AddScoped<ICarreraValidator, ValidateCarrera>();


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<SistemaTurnosOnlineDbContext>();

//! Enum route constraints: ref https://nickheath.net/2019/02/20/asp-net-core-enum-route-constraints/
builder.Services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options =>
{
    options.ConstraintMap.Add("AttributeCheck", typeof(SistemaTurnosOnline.Api.Extensions.CustomRouteConstraint<ProfesorParam.Field>));
});

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
