using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using PruebaTecnica.App;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Services;
using PruebaTecnica.App.Services.Security;
using System;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped<Conexion>();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
        options.SlidingExpiration = true; // Renovar la sesión con cada solicitud
        options.Cookie.HttpOnly = true; // Asegura que las cookies de sesión no sean accesibles desde el cliente
        options.Cookie.IsEssential = true; // Necesario para que la sesión funcione incluso si el usuario no consiente el seguimiento
    });

builder.Services.AddScoped<UserAdministrationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
