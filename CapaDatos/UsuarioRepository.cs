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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbcarritoContext _context;

        public UsuarioRepository(DbcarritoContext context)
        {
            _context = context;
        }

        public async Task<(int Resultado, string Mensaje)> RegistrarUsuarioAsync(Usuario usuario)
        {
            var resultadoParam = new SqlParameter("@resultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var mensajeParam = new SqlParameter("@mensaje", System.Data.SqlDbType.VarChar, 100)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            var token = Guid.NewGuid().ToString();
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegistraeUsuario @nombres, @apellidos, @correo, @clave, @Activo, @Token, @resultado OUTPUT, @mensaje OUTPUT",
                new SqlParameter("@nombres", usuario.Nombres),
                new SqlParameter("@apellidos", usuario.Apellidos),
                new SqlParameter("@correo", usuario.Correo),
                new SqlParameter("@clave", usuario.Clave),
                new SqlParameter("@Activo", usuario.Activo),
                new SqlParameter("@Token", usuario.Token = token),
                resultadoParam,
                mensajeParam
            );

            return (Convert.ToInt32(resultadoParam.Value), mensajeParam.Value.ToString());
        }

        public async Task<int> ValidarCorreo(Usuario usuario)
        {
            var resultadoParam = new SqlParameter("@resultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_validarCorreo @correo, @resultado OUTPUT",
                new SqlParameter("@correo", usuario.Correo),
                resultadoParam
                );
            return (Convert.ToInt32(resultadoParam.Value));
        }
        public async Task<(int Resultado, string Mensaje)> EditarUsuarioAsync(Usuario usuario)
        {
            var resultadoParam = new SqlParameter("@resultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var mensajeParam = new SqlParameter("@mensaje", System.Data.SqlDbType.VarChar, 100)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_editar @id_Usuario, @nombres, @apellidos, @correo, @activo, @mensaje OUTPUT, @resultado OUTPUT",
                new SqlParameter("@id_Usuario", usuario.IdUsuario),
                new SqlParameter("@nombres", usuario.Nombres),
                new SqlParameter("@apellidos", usuario.Apellidos),
                new SqlParameter("@correo", usuario.Correo),
                new SqlParameter("@activo", usuario.Activo),
                resultadoParam,
                mensajeParam
            );

            return (Convert.ToInt32(resultadoParam.Value), mensajeParam.Value.ToString());
        }

    }
}
