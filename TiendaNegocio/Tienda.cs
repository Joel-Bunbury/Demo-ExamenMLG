using TiendaEntidades;
using TiendaDatos;

namespace TiendaNegocio
{
    public static class Tienda
    {
        public static List<TiendaEntidades.Tienda> ConsultarTiendas(string sucursal)
        {
            return TiendaDatos.Tienda.ConsultarTiendas(sucursal);
        }

        public static async Task<TiendaEntidades.Tienda> BuscarPorId(int IdTienda)
        {
            return await TiendaDatos.Tienda.BuscarPorId(IdTienda);
        }

        public static async Task<int> CrearTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            return await TiendaDatos.Tienda.CrearTiendaAsync(_tienda);
        }

        public static async Task<int> EditarTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            return await TiendaDatos.Tienda.EditarTiendaAsync(_tienda);
        }

        public static async Task<int> EliminarTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            return await TiendaDatos.Tienda.EliminarTiendaAsync(_tienda);
        }

    }
}