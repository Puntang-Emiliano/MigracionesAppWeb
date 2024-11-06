using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pasajeApp.Datos.Data.Repository;
using pasajeApp.Datos.Data.Repository.IRepository;
using PasajesApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders() // Agrega esta línea para los tokens predeterminados
    .AddDefaultUI();
builder.Services.AddControllersWithViews();
//Agrego linea de codigo para la inyeccion de Independencias
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    pattern: "{area=Cliente}/{controller=Articulo}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
