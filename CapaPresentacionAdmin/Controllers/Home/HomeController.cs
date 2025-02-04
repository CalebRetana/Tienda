using CapaDatos;
using CapaNegocio;
using CapaPresentacionAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace CapaPresentacionAdmin.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly DbcarritoContext _context;
        private readonly IndexService _indexService;

        public HomeController(DbcarritoContext context, IndexService indexService)
        {
            _context = context;
            _indexService = indexService;
        }
        public async Task<IActionResult> Index()
        {
            var (cantidadClientes, cantidadVentas, cantidadProductos) = await _indexService.ObtenerIndexAsync();

            // Pasar los datos directamente a la vista usando ViewBag o ViewData
            ViewBag.CantidadClientes = cantidadClientes;
            ViewBag.CantidadVentas = cantidadVentas;
            ViewBag.CantidadProductos = cantidadProductos;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
