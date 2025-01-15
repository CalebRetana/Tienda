using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.CapaEntidades;
using CapaDatos;

namespace CapaPresentacionAdmin.Controllers.Mantenedor
{
    public class MarcasController : Controller
    {
        private readonly DbcarritoContext _context;

        public MarcasController(DbcarritoContext context)
        {
            _context = context;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Marcas.ToListAsync());
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }
        public async Task<bool> existeMarca(Marca marca)
        {
            var existe = await _context.Marcas
                             .FirstOrDefaultAsync(m => m.Descripcion == marca.Descripcion && m.IdMarca != marca.IdMarca);
            if (existe == null)
            {
                return false;
            }

            else return true;
        }
        // POST: Marcas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMarca,Descripcion,Activo,FechaRegistro")] Marca marca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existe = await existeMarca(marca);
                    if (!existe)
                    {
                        _context.Add(marca);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"La marca ya existe");
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
                return View(marca);
            }
            return View(marca);

        }

        // GET: Marcas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMarca,Descripcion,Activo,FechaRegistro")] Marca marca)
        {
            try
            {
                if (id != marca.IdMarca)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    var existe = await existeMarca(marca);
                    if (!existe)
                    {
                        try
                        {
                            _context.Update(marca);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!MarcaExists(marca.IdMarca))
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"La marca ya existe");
                        return View(marca);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
                return View(marca);
            }
            return View(marca);
        }

        // GET: Marcas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
            return _context.Marcas.Any(e => e.IdMarca == id);
        }
    }
}
