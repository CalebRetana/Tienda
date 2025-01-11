using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public interface ICategoriasRepository
    {
        Task<int> ValidarCategoria(Categorium categoria);
        Task<int> ValidarEliminar(Categorium categoria);
    }
}
