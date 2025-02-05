using CapaDominio;
using CapaEntidades.CapaEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ReporteVentasRepository : IReporteVentasRepository
    {
        private readonly DbcarritoContext _context;

        public ReporteVentasRepository(DbcarritoContext context)
        {
            _context = context;
        }
        public async Task<int> Ventas(string fechaInicio, string fechaFin, string IdTransaccion)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC SP_reporteVentas @FechaInicio, @FechaFin, @id_transaccion",
                new SqlParameter("@FechaInicio", fechaInicio),
                new SqlParameter("@FechaFin", fechaFin),
                new SqlParameter("@id_transaccion", IdTransaccion)
            );

            return result; // Retorna el número de filas afectadas por la ejecución del procedimiento almacenado
        }

    }
}
