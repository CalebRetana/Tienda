using CapaDominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VentaRepository : IVentaRepository
    {
        private readonly DbcarritoContext _context;

        public VentaRepository(DbcarritoContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Venta.CountAsync();
        }
    }
}
