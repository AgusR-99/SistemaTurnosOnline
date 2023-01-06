using FluentValidation;
using SistemaTurnosOnline.Models;
using SistemaTurnosOnline.Models.Validators;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;
using SistemaTurnosOnline.Models.Validators.Contracts;
using Blazorise;
using SistemaTurnosOnline.Shared.Validators;
using SistemaTurnosOnline.Shared.Validators.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7184") });
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddTransient<ICarreraService, CarreraService>();
builder.Services.AddTransient<IValidateProfesor, ValidateProfesorService>();
builder.Services.AddTransient<ICarreraValidator, ValidateCarreraService>();
builder.Services.AddScoped<IValidator<ProfesorForm>, ProfesorValidator>();
builder.Services.AddScoped<IValidator<Carrera>, CarreraValidator>();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
