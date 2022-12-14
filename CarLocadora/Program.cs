using CarLocadora.Extensoes;
using CarLocadora.Modelo.Models;
using CarLocadora.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarServicos();

builder.Services.ConfigurarCookiePolicy();
builder.Services.ConfigurarAuthentication();

builder.Services.ConfiguraAPI(builder.Configuration);
// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
