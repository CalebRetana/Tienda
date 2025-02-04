using Microsoft.EntityFrameworkCore;
using CapaNegocio;
using CapaDatos;
using CapaDominio;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración del DbContext
builder.Services.AddDbContext<DbcarritoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

var cloudinaryConfig = builder.Configuration.GetSection("Cloudinary");

var cloudinary = new Cloudinary(new Account(
    cloudinaryConfig["CloudName"],
    cloudinaryConfig["ApiKey"],
    cloudinaryConfig["ApiSecret"]
    ));

builder.Services.AddSingleton(cloudinary);
// Registro de las capas de datos y negocio
builder.Services.AddScoped<UsuarioService>(); 
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>(); 
builder.Services.AddScoped<IProductoRepository, ProductoRepository>(); 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>(); 
builder.Services.AddScoped<IVentaRepository, VentaRepository>(); 
builder.Services.AddScoped<CategoriaService>(); 
builder.Services.AddScoped<MarcasService>();
builder.Services.AddScoped<IndexService>();
builder.Services.AddScoped<ProductosService>();
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
