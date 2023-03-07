using FluentValidation;
using SistemaTurnosOnline.Web.Services;
using SistemaTurnosOnline.Web.Services.Contracts;
using Blazorise;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SistemaTurnosOnline.Shared.Validators;
using SistemaTurnosOnline.Shared.Validators.Contracts;
using SistemaTurnosOnline.Shared;
using SistemaTurnosOnline.Shared.Turnos;
using SistemaTurnosOnline.Web.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using SistemaTurnosOnline.Web.Hubs;
using SistemaTurnosOnline.Web.Hubs.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7184") });

builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddTransient<ICarreraService, CarreraService>();
builder.Services.AddTransient<ITurnoService, TurnoService>();
builder.Services.AddTransient<ITurnoHubClient, TurnoHubClient>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddTransient<IValidateProfesor, ValidateProfesorService>();
builder.Services.AddTransient<ICarreraValidator, ValidateCarreraService>();
builder.Services.AddTransient<ISignInValidator, ValidateSignInService>();
builder.Services.AddTransient<ITurnoValidator, ValidateTurnoService>();
builder.Services.AddTransient<IPasswordValidator, ValidateProfileSecurity>();

builder.Services.AddScoped<IValidator<ProfesorForm>, ProfesorValidator>();
builder.Services.AddScoped<IValidator<ProfesorSecure>, ProfesorSecureValidator>();
builder.Services.AddScoped<IValidator<Carrera>, CarreraValidator>();
builder.Services.AddScoped<IValidator<SignInForm>, SignInValidator>();
builder.Services.AddScoped<IValidator<TurnoListado>, TurnoValidator>();
builder.Services.AddScoped<IValidator<ProfileSecurityForm>, ProfileSecurityValidator>();

builder.Services.AddResponseCompression(o =>
{
    o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" }
        );
});

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
app.MapHub<InfoHub>("/inactiveUsersHub");
app.MapHub<TurnoHub>("/turnohub");
app.MapHub<TurnoHub>("/turnoqueuehub");
app.MapFallbackToPage("/_Host");

app.Run();
