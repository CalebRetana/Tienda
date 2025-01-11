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
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly DbcarritoContext _context;
        public CategoriasRepository(DbcarritoContext context) 
        {
            _context = context;
        }

        public async Task<int> ValidarCategoria(Categorium categoria)
        {
            var resultadoParam = new SqlParameter("@resultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_validarCategoria @descripcion, @resultado OUTPUT",
                new SqlParameter("@descripcion", categoria.Descripcion),
                resultadoParam
                );
            return (Convert.ToInt32(resultadoParam.Value));
        }

        public async Task<int> ValidarEliminar(Categorium categorium)
        {
            var resultadoParam = new SqlParameter("@Resultado", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ValidarEliminar @id_categoria, @Resultado OUTPUT",
                new SqlParameter("@id_categoria", categorium.IdCategoria),
                resultadoParam
            );
            return Convert.ToInt32(resultadoParam.Value);
        }
    }
}
