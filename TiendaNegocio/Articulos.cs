using TiendaEntidades;
using TiendaDatos;

namespace TiendaNegocio
{
    public static class Articulos
    {

        public static async Task<List<TiendaEntidades.Articulo>> ConsultarArticulos(int IdTienda)
        {
            return await TiendaDatos.Articulos.ConsultarArticulos(IdTienda);
        }

        public static async Task<List<TiendaEntidades.Articulo>> ConsultarArticulosCompra()
        {
            return await TiendaDatos.Articulos.ConsultarArticulosCompra();
        }

        public static async Task<TiendaEntidades.Articulo> BuscarPorId(Guid IdCliente, int IdTienda) 
        {
            return await TiendaDatos.Articulos.BuscarPorId(IdCliente,IdTienda);
        }

        public static async Task<TiendaEntidades.Articulo> BuscarPorId(Guid IdCliente)
        {
            return await TiendaDatos.Articulos.BuscarPorId(IdCliente);
        }

        public static async Task<int> CrearArticuloAsync(TiendaEntidades.Articulo _articulo)
        {
            return await TiendaDatos.Articulos.CrearArticuloAsync(_articulo);
        }

        public static async Task<int> EditarArticuloAsync(TiendaEntidades.Articulo _articulo)
        {
            return await TiendaDatos.Articulos.EditarArticuloAsync(_articulo);
        }

        public static async Task<int> EliminarArticuloAsync(TiendaEntidades.Articulo _articulo)
        {
            return await TiendaDatos.Articulos.EliminarArticuloAsync(_articulo);
        }

        public static async Task<int> AgregarCarritoCompra(TiendaEntidades.CarritoDescripcion carrito)
        {
            return await TiendaDatos.Articulos.AgregarCarritoCompra(carrito);
        }

        public static async Task<List<CarritoDescripcion>> ConsultarArticulosCliente(Guid IdCliente)
        {
            return await TiendaDatos.Articulos.ConsultarArticulosCliente(IdCliente);
        }

        public static async Task<int> PagarArticulosCliente(Guid IdCliente)
        {
            return await TiendaDatos.Articulos.PagarArticulosCliente(IdCliente);
        }

    }
}