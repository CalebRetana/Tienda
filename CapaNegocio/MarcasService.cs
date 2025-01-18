using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MarcasService
    {
        public Task<(int resultado, string mensaje)> validaCamposVacios(Marca marcas)
        {
            if (string.IsNullOrEmpty(marcas.Descripcion))
            {
                return Task.FromResult((0, "La descripción no puede estar vacía."));
            }

            return Task.FromResult((1, string.Empty));
        }



    }
}
