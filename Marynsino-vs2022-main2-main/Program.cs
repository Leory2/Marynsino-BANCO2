using Microsoft.EntityFrameworkCore;
using Marynsino2513.Models;
using Marynsino2513.ORM;
using Marynsino2513.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Registrar o DbContext se necessário
builder.Services.AddDbContext<BdMarynsinoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar o repositório (UsuarioRepositorio)
builder.Services.AddScoped<UsuarioRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso
builder.Services.AddScoped<ServicoRepositorio>();
// Registrar o repositório (UsuarioRepositorio)
//builder.Services.AddScoped<ServicoRepositorio>();  // Ou AddTransient ou AddSingleton dependendo do caso

// Registrar outros serviços, como controllers com views
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

