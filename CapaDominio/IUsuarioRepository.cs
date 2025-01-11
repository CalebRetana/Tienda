using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public interface IUsuarioRepository
    {
        Task<(int Resultado, string Mensaje)> RegistrarUsuarioAsync(Usuario usuario);
        Task<int> ValidarCorreo(Usuario usuario);
        Task<(int Resultado, string Mensaje)> EditarUsuarioAsync(Usuario usuario);

    }
}
