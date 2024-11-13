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

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders() // Agrega esta línea para los tokens predeterminados
//    .AddDefaultUI();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider; // Agrega el proveedor de tokens para confirmación de email
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() // Agrega los proveedores de tokens predeterminados
.AddDefaultUI();

builder.Services.AddControllersWithViews();
//Agrego linea de codigo para la inyeccion de Independencias
builder.Services.AddScoped<IContenedorTrabajo, ContenedorTrabajo>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();            //ver esto  
builder.Services.AddScoped<ICarritoItemRepository, CarritoItemRepository>();    //ver esto
builder.Services.AddScoped<IArticuloRepository, ArticuloRepository>();          //ver esto



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

//app.UseSession();

app.UseRouting();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
