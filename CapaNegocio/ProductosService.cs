using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductosService
    {
        public Task<(int resultado, string mensaje)> validaCamposVacios(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Descripcion))
            {
                return Task.FromResult((0, "La descripción no puede estar vacía."));
            }
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                return Task.FromResult((0, "El nombre no puede estar vacía."));
            }
            if (producto.Stock<0 || producto.Stock == null)
            {
                return Task.FromResult((0, "Agrega una cantidad al stock"));
            }

            return Task.FromResult((1, string.Empty));
        }

    }
}
