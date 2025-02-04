using CapaDatos;
using CapaDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class IndexService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;

        public IndexService(
            IClienteRepository clienteRepository,
            IVentaRepository ventaRepository,
            IProductoRepository productoRepository)
        {
            _clienteRepository = clienteRepository;
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
        }

        public async Task<(int cantidadClientes, int cantidadVentas, int cantidadProductos)> ObtenerIndexAsync()
        {
            var cantidadClientes = await _clienteRepository.CountAsync();
            var cantidadVentas = await _ventaRepository.CountAsync();
            var cantidadProductos = await _productoRepository.CountAsync();

            return (cantidadClientes, cantidadVentas, cantidadProductos);
        }
    }
}
