using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public interface IReporteVentasRepository
    {
        Task<int> Ventas(string fechaInicio, string fechaFin, string IdTransaccion)
    }
}
