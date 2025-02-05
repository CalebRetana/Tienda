using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ReporteVentasService
    {
        private readonly IReporteVentasRepository _reporteVentasRepository;

        public ReporteVentasService(IReporteVentasRepository reporteVentasRepository)
        {
            _reporteVentasRepository = reporteVentasRepository;
        }
        public async Task<int> Ventas(string fechaInicio, string fechaFin, string IdTransaccion)
        {
            return await _reporteVentasRepository.Ventas(fechaInicio, fechaFin, IdTransaccion);
        }

    }
}
