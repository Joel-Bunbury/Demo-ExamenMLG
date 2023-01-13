using Microsoft.EntityFrameworkCore;
using TiendaDatos.Models;
using TiendaEntidades;

namespace TiendaDatos
{
    public class Tienda
    {
        public static List<TiendaEntidades.Tienda> ConsultarTiendas(string persona)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var tiendas = contexto.Tienda
                    .Where(t => t.Activo == true)
                    .Select ( t => new TiendaEntidades.Tienda
                    {
                        IdTienda = t.IdTienda,
                        Sucursal = t.Sucursal,
                        Direccion = t.Direccion
                    }).ToList();
                return tiendas;
            }
        }

        public static async Task<TiendaEntidades.Tienda> BuscarPorId(int IdTienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var tienda = await contexto.Tienda
                     .Where(t => t.Activo == true && t.IdTienda == IdTienda)
                     .Select(t => new TiendaEntidades.Tienda
                     {
                         IdTienda = t.IdTienda,
                         Sucursal = t.Sucursal,
                         Direccion = t.Direccion
                     }).FirstOrDefaultAsync();
                return tienda;
            }
        }

        public static async Task<int> CrearTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var tienda = new Tiendum()
                {
                    Sucursal = _tienda.Sucursal,
                    Direccion = _tienda.Direccion,
                    Activo = true
                };

                contexto.Add(tienda);

                int writtenEntriesCount = await contexto.SaveChangesAsync();
                if (writtenEntriesCount > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static async Task<int> EditarTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var tienda = contexto.Tienda.Find(_tienda.IdTienda);
                tienda.Sucursal = _tienda.Sucursal;
                tienda.Direccion = _tienda.Direccion;

                int writtenEntriesCount = await contexto.SaveChangesAsync();
                if (writtenEntriesCount > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static async Task<int> EliminarTiendaAsync(TiendaEntidades.Tienda _tienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var tienda = contexto.Tienda.Find(_tienda.IdTienda);
                tienda.Activo = false;
                //contexto.Tienda.Remove(tienda);

                int writtenEntriesCount = await contexto.SaveChangesAsync();
                if (writtenEntriesCount > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}