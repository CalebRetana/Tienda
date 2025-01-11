using Microsoft.EntityFrameworkCore;
using CapaNegocio;
using CapaDatos;
using CapaDominio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración del DbContext
builder.Services.AddDbContext<DbcarritoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

// Registro de las capas de datos y negocio
builder.Services.AddScoped<UsuarioService>(); 
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>(); // Registro de interfaz e implementación
builder.Services.AddScoped<CategoriaService>(); // El servicio de negocio que depende de ICategoriasRepository
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
