using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.CapaEntidades;
using CapaDatos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers.Mantenedor
{
    public class ProductoesController : Controller
    {
        private readonly DbcarritoContext _context;
        private readonly Cloudinary _cloudinary;
        private readonly ProductosService _productosService;
        public ProductoesController(DbcarritoContext context, Cloudinary cloudinary, ProductosService productosService)
        {
            _context = context;
            _cloudinary = cloudinary;
            _productosService = productosService;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var dbcarritoContext = _context.Productos.Include(p => p.categoria).Include(p => p.Marca);
            return View(await dbcarritoContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Precio,Stock,RutaImagen,NombreImagen,Activo,FechaRegistro,IdMarca,IdCategoria")] Producto producto, IFormFile file)
        {
            try
            {
                var (respuesta, mensaje) = await _productosService.validaCamposVacios(producto);
                if (respuesta != 1)
                {
                    //Regresa a la vista con los datos del producto
                    ModelState.AddModelError(string.Empty, mensaje);
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
                    ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
                    return View(producto);
                }

                // Verifica que se haya seleccionado una imagen
                if (file == null || file.Length == 0)
                {
                    ModelState.AddModelError(string.Empty, "Error al subir la imagen. (Pueda ser que no hayas seleccionado alguna)");
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
                    ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
                    return View(producto);
                }

                // Validación del tipo de archivo (solo imágenes PNG, JPG y JPEG)
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError(string.Empty, "Solo se permiten imágenes en formato PNG, JPG o JPEG.");
                    ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion", producto.IdCategoria);
                    ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion", producto.IdMarca);
                    return View(producto);
                }

                if (ModelState.IsValid)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        //Archivo que estamos intentando subir
                        File = new FileDescription(file.FileName, file.OpenReadStream()),

                        //Indicamos donde queremos guardar las imagenes (Es una carpeta que cree en Cloudinary)
                        AssetFolder = "ProductsImages"
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    // Asignamos la URL segura y la ruta de la imagen al producto
                    producto.RutaImagen = uploadResult.SecureUrl.ToString();
                    producto.NombreImagen = file.FileName;

                    _context.Add(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,Precio,Stock,RutaImagen,NombreImagen,Activo,FechaRegistro,IdMarca,IdCategoria")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Descripcion");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "IdMarca", "Descripcion");
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}