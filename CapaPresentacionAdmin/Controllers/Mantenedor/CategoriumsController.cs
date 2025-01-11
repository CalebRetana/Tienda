using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapaEntidades.CapaEntidades;
using CapaDatos;
using CapaNegocio;

namespace CapaPresentacionAdmin.Controllers.Mantenedor
{
    public class CategoriumsController : Controller
    {
        private readonly DbcarritoContext _context;
        private readonly CategoriaService _categoria;
        public CategoriumsController(DbcarritoContext context, CategoriaService categoria)
        {
            _context = context;
            _categoria = categoria;
        }

        // GET: Categoriums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        // GET: Categoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,Descripcion,Activo,FechaRegistro")] Categorium categorium)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resultado = await _categoria._validarCategoria(categorium);
                    if (resultado == 1)
                    {
                        _context.Add(categorium);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"La categoria ya existe");
                    }


                }
            } catch (Exception ex) {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
            }
            return View(categorium);
        }

        // GET: Categoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria.FindAsync(id);
            if (categorium == null)
            {
                return NotFound();
            }
            return View(categorium);
        }

        // POST: Categoriums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Descripcion,Activo,FechaRegistro")] Categorium categorium)
        {
            if (id != categorium.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriumExists(categorium.IdCategoria))
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
            return View(categorium);
        }

        // GET: Categoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categoria
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        // POST: Categoriums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Categorium categoria)
        {
            try
            {
                var categorium = await _context.Categoria.FindAsync(id);
                var resultado = await _categoria.EliminarCategoria(categoria);
                if(resultado == 1)
                {
                    if (categorium != null)
                    {
                        _context.Categoria.Remove(categorium);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               else
               {
                  ModelState.AddModelError(string.Empty, $"No puedes eliminar. Ya has agreagdo productos, trata desactivando esta categoria");
               }

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, $"Ocurrió un error: {ex.Message}");
            }
            return View();
        }

        private bool CategoriumExists(int id)
        {
            return _context.Categoria.Any(e => e.IdCategoria == id);
        }
    }
}
