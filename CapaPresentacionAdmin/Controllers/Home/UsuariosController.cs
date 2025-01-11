using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.CapaEntidades;
using CapaNegocio;
using CapaDatos;
namespace CapaPresentacionAdmin.Controllers.Home
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioService _cnUsuarios;
        private readonly DbcarritoContext _context;

        public UsuariosController(UsuarioService cnUsuarios, DbcarritoContext context)
        {
            _cnUsuarios = cnUsuarios;
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,Apellidos,Correo,Clave")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    usuario.Activo = false; 
                    usuario.Token = Guid.NewGuid().ToString(); 

                    
                    var (resultado, mensaje) = await _cnUsuarios.RegistrarUsuario(usuario);

                    if (resultado > 0)
                    {
                        TempData["SuccessMessage"] = "Usuario registrado con éxito.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();

            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Este método edita desde un procedimiento almacenado, tiene interacción con la capa de negocios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombres,Apellidos,Correo,Clave,Reestablecer,Activo,FechaRegistro")] Usuario usuario)
        {
            try
            {

                if (id != usuario.IdUsuario)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var (resultado, mensaje) = await _cnUsuarios.EditarUsuario(usuario);

                    if (resultado > 0)
                    {
                        TempData["SuccessMessage"] = "Usuario actualizado con éxito.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, mensaje);
                    }
                }

            }catch(Exception ex) 
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
            }
            return View(usuario);
        }
                 
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
