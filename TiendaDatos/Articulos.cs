using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TiendaDatos.Models;
using TiendaEntidades;

namespace TiendaDatos
{
    public class Articulos
    {
        public static async Task<List<TiendaEntidades.Articulo>> ConsultarArticulos(int IdTienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulos = await (from ta in contexto.TiendaArticulos
                                 join a in contexto.Articulos on ta.IdArticulo equals a.IdArticulo
                                 where ta.IdTienda == IdTienda && a.Activo == true
                                 select new TiendaEntidades.Articulo
                                 {
                                     IdArticulo = a.IdArticulo,
                                     Codigo = a.Codigo,
                                     Descripcion = a.Descripcion,
                                     Precio = (decimal)a.Precio,
                                     Imagen = a.Imagen,
                                     Stock = (int)a.Stock,
                                 }).ToListAsync();
                return articulos;
            }
        }

        public static async Task<List<TiendaEntidades.Articulo>> ConsultarArticulosCompra()
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulos = await contexto.Articulos
                    .Where(a => a.Activo == true && a.Stock > 0)
                    .Select(a => new TiendaEntidades.Articulo
                    {
                        IdArticulo = a.IdArticulo,
                        Codigo = a.Codigo,
                        Descripcion = a.Descripcion,
                        Precio = (decimal)a.Precio,
                        Imagen = a.Imagen,
                        Stock = (int)a.Stock,
                    }).ToListAsync();
                return articulos;
            }
        }

        public static async Task<TiendaEntidades.Articulo> BuscarPorId(Guid IdArticulo, int IdTienda)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulo = await (from ta in contexto.TiendaArticulos
                                       join a in contexto.Articulos on ta.IdArticulo equals a.IdArticulo
                                       where ta.IdTienda == IdTienda && a.IdArticulo == IdArticulo && a.Activo == true
                                       select new TiendaEntidades.Articulo
                                       {
                                           IdArticulo = a.IdArticulo,
                                           Codigo = a.Codigo,
                                           Descripcion = a.Descripcion,
                                           Precio = (decimal)a.Precio,
                                           Imagen = a.Imagen,
                                           Stock = (int)a.Stock,
                                           IdTienda = ta.IdTienda
                                       }).FirstOrDefaultAsync();
                return articulo;
            }
        }

        public static async Task<TiendaEntidades.Articulo> BuscarPorId(Guid IdArticulo)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulo = await (from ta in contexto.TiendaArticulos
                                      join a in contexto.Articulos on ta.IdArticulo equals a.IdArticulo
                                      where  a.IdArticulo == IdArticulo && a.Activo == true
                                      select new TiendaEntidades.Articulo
                                      {
                                          IdArticulo = a.IdArticulo,
                                          Codigo = a.Codigo,
                                          Descripcion = a.Descripcion,
                                          Precio = (decimal)a.Precio,
                                          Imagen = a.Imagen,
                                          Stock = (int)a.Stock
                                      }).FirstOrDefaultAsync();
                return articulo;
            }
        }

        public static async Task<int> CrearArticuloAsync(TiendaEntidades.Articulo articulo)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulodb = new TiendaDatos.Models.Articulo()
                {
                    IdArticulo = Guid.NewGuid(),
                    Codigo = articulo.Codigo,
                    Descripcion = articulo.Descripcion,
                    Precio = articulo.Precio,
                    Stock = articulo.Stock,
                    Imagen = articulo.Imagen,
                    Activo = true
                };

                contexto.Add(articulodb);

                var relacionTiendaArticulo = new TiendaDatos.Models.TiendaArticulo()
                {
                    IdArticulo = articulodb.IdArticulo,
                    IdTienda = articulo.IdTienda,
                    Fecha = DateTime.Now,
                };

                contexto.Add(relacionTiendaArticulo);

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

        public static async Task<int> EditarArticuloAsync(TiendaEntidades.Articulo articulo)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {

                var articuloDb = contexto.Articulos.Find(articulo.IdArticulo);
                articuloDb.Codigo = articulo.Codigo;
                articuloDb.Descripcion = articulo.Descripcion;
                articuloDb.Precio = articulo.Precio;
                articuloDb.Stock = articulo.Stock;
                articuloDb.Imagen = articulo.Imagen;

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

        public static async Task<int> EliminarArticuloAsync(TiendaEntidades.Articulo articulo)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articuloDB = contexto.Articulos.Find(articulo.IdArticulo);
                articuloDB.Activo = false;
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

        public static async Task<int> AgregarCarritoCompra(TiendaEntidades.CarritoDescripcion carrito)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulo = await contexto.Articulos.FindAsync(carrito.IdArticulo);
                
                var articuloAgre = await contexto.ClienteArticulos
                    .Where(ca => ca.IdArticulo == carrito.IdArticulo 
                        && ca.IdCliente == carrito.IdCliente
                        && ca.Pagado == false)
                    .FirstOrDefaultAsync();

                if(articuloAgre == null)
                {
                    var carritodb = new TiendaDatos.Models.ClienteArticulo()
                    {
                        IdCliente = carrito.IdCliente,
                        IdArticulo = carrito.IdArticulo,
                        Fecha = DateTime.Now,
                        Cantidad = carrito.Cantidad,
                        Total = carrito.Cantidad * articulo.Precio,
                        Pagado = false
                    };

                    contexto.Add(carritodb);
                }
                else
                {
                    articuloAgre.Cantidad = articuloAgre.Cantidad + carrito.Cantidad;
                    articuloAgre.Total = articuloAgre.Cantidad * articulo.Precio;
                }
                
               

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

        public static async Task<List<TiendaEntidades.CarritoDescripcion>> ConsultarArticulosCliente(Guid IdCliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulos = await contexto.ClienteArticulos
                    .Where(a => a.Pagado == false && a.IdCliente == IdCliente)
                    .Select(a => new TiendaEntidades.CarritoDescripcion
                    {
                        IdCliente = a.IdCliente,
                        IdArticulo = a.IdArticulo,
                        Fecha = a.Fecha,
                        Cantidad = (int)a.Cantidad,
                        Total = (decimal)a.Total,
                        Pagado = (bool)a.Pagado,
                    }).ToListAsync();

                var articulosDB = await contexto.Articulos.Select( a => new TiendaEntidades.Articulo
                {
                    IdArticulo = a.IdArticulo,
                    Codigo = a.Codigo,
                    Descripcion = a.Descripcion,
                    Precio = (decimal)a.Precio,
                    Imagen = a.Imagen,
                    Stock = (int)a.Stock
                }).ToListAsync();

                articulos.ForEach(x =>
                {
                    x.Articulo = articulosDB.Where(i => i.IdArticulo.Equals(x.IdArticulo)).First();
                });
                
                return articulos;
            }
        }

        public static async Task<int> PagarArticulosCliente(Guid IdCliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var articulos = await contexto.ClienteArticulos
                    .Where(a => a.Pagado == false && a.IdCliente == IdCliente).ToListAsync();

                articulos.ForEach(a =>{
                    a.Pagado = true;
                });

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