using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using PruebaTecnica.App;
using PruebaTecnica.App.Database;
using PruebaTecnica.App.Pages;
using PruebaTecnica.App.Services;
using PruebaTecnica.App.Services.Security;
using PruebaTecnica.App.Services.Security.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<Conexion>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<CustomAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
builder.Services.AddScoped<AuthService>();


// Autenticación y Autorización
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
    });

    
builder.Services.AddScoped<UserAdministrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.MapRazorPages();

/*app.MapBlazorHub();
app.MapFallbackToPage("/App/Pages/_Host");*/

app.Run();
