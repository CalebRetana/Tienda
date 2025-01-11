using CapaEntidades.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaDominio;
namespace CapaNegocio
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _UsuariosReposity;

        public UsuarioService(IUsuarioRepository _cdUsuarios)
        {
            _UsuariosReposity = _cdUsuarios;
        }

        public async Task<(int resultado, string mensaje)> RegistrarUsuario(Usuario usuario)
        {
           
            if (string.IsNullOrEmpty(usuario.Nombres))
            {
                return (0, "El nombre no puede estar vacío.");
            }
            if(string.IsNullOrEmpty(usuario.Apellidos))
            {
                
                return (0, "El apellido no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(usuario.Correo))
            {
                
                return (0, "El correo no puede estar vacío.");
            }
                int validacion = await _UsuariosReposity.ValidarCorreo(usuario);
                string clave = RecursosService.generarClave();
                string asunto = "CLAVE TIENDA EL BARATILLO";
                string mensaje = $"<h3>SU CUENTA FUE CREADA CORRECTAMENTE</h3></br><p>Inicia sesión con esta clave: {clave}</p>";
            if (validacion == 1)
            {
                bool respuesta = RecursosService.EnviarCorreo(usuario.Correo, asunto, mensaje);

                if (respuesta)
                {
                    usuario.Clave = RecursosService.Encriptar256(clave);
                    return await _UsuariosReposity.RegistrarUsuarioAsync(usuario);

                }
                else
                {
                    return (0, "Problemas al enviar el correo");
                }
            }
            else
            {
                return (0, "El correo ya existe");
            }
        }

        public async Task<(int resultado, string mensaje)> EditarUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombres))
            {
                return (0, "El nombre no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(usuario.Apellidos))
            {
                return (0, "El apellido no puede estar vacío.");
            }
            if (string.IsNullOrEmpty(usuario.Correo))
            {
                return (0, "El correo no puede estar vacío.");
            }
            return await _UsuariosReposity.EditarUsuarioAsync(usuario);
        }
    }
}
