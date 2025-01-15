using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaEntidades.CapaEntidades;

namespace CapaNegocio
{
    public class CategoriaService
    {
        private readonly ICategoriasRepository _categoriaRepository;

        public CategoriaService(ICategoriasRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        //Si devuelve 1 se hará, sino es porque ya existe una categoria bajo el mismo nombre
        public async Task<int> _validarCategoria(Categorium categoria)
        {
            int validacion = await _categoriaRepository.ValidarCategoria(categoria);
            return validacion;
        }
        //Si devuelve 1 se hará, sino es porque ya existe un producto bajo la categoria seleccionada
        public async Task<int> EliminarCategoria(Categorium categoria)
        {
            int validacion = await _categoriaRepository.ValidarEliminar(categoria);
            return validacion;
        }
        public async Task<(int resultado, string mensaje)> validaCamposVacios(Categorium categoria)
        {
            if (string.IsNullOrEmpty(categoria.Descripcion))
            {
                return (0, "La descripción no puede estar vacía.");
            }
            if (categoria.Activo == null)
            {
                return (0, "Debe seleccionar una opción para activo");
            }
            return (1, string.Empty);
        }
    }
}
