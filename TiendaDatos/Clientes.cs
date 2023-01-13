using Microsoft.EntityFrameworkCore;
using TiendaDatos.Models;
using TiendaEntidades;

namespace TiendaDatos
{
    public class Clientes
    {
        public static async Task<TiendaEntidades.Cliente> Login(TiendaEntidades.Login login)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var pass = Utilidades.Encriptar(login.Password); 
                var cliente = await contexto.Clientes
                    .Where(c => c.Activo == true && c.UserName == login.UserName && c.Password == pass)
                    .Select ( c => new TiendaEntidades.Cliente
                    {
                        IdCliente = c.IdCliente,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoMaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                        Direccion = c.Direccion
                    }).FirstOrDefaultAsync();
                return cliente;
            }
        }

        public static async Task<List<TiendaEntidades.Cliente>> ConsultarClientes(string persona)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var clientes = await contexto.Clientes
                    .Where(c => c.Activo == true)
                    .Select(c => new TiendaEntidades.Cliente
                    {
                        IdCliente = c.IdCliente,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoPaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                        Direccion = c.Direccion
                    }).ToListAsync();
                return clientes;
            }
        }

        public static async Task<TiendaEntidades.Cliente> BuscarPorId(Guid IdCliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var cliente = await contexto.Clientes
                     .Where(c => c.Activo == true && c.IdCliente == IdCliente)
                     .Select(c => new TiendaEntidades.Cliente
                     {
                         IdCliente = c.IdCliente,
                         Nombre = c.Nombre,
                         ApellidoPaterno = c.ApellidoPaterno,
                         ApellidoMaterno = c.ApellidoMaterno,
                         UserName = c.UserName,
                         Password = Utilidades.Desencriptar(c.Password),
                         Direccion = c.Direccion
                     }).FirstOrDefaultAsync();
                return cliente;
            }
        }

        public static async Task<int> CrearClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var pass = Utilidades.Encriptar(_cliente.Password); 

                var cliente = new TiendaDatos.Models.Cliente()
                {
                    IdCliente = Guid.NewGuid(),
                    Nombre = _cliente.Nombre,
                    ApellidoPaterno = _cliente.ApellidoPaterno,
                    ApellidoMaterno= _cliente.ApellidoMaterno,
                    Direccion = _cliente.Direccion,
                    UserName = _cliente.UserName,
                    Password = pass,
                    Activo = true
                };

                contexto.Add(cliente);

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

        public static async Task<int> EditarClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var pass = Utilidades.Encriptar(_cliente.Password);

                var cliente = contexto.Clientes.Find(_cliente.IdCliente);
                cliente.Nombre = _cliente.Nombre;
                cliente.ApellidoPaterno = _cliente.ApellidoPaterno;
                cliente.ApellidoMaterno = _cliente.ApellidoMaterno;
                cliente.UserName = _cliente.UserName;
                cliente.Password = pass;
                cliente.Direccion = _cliente.Direccion;

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

        public static async Task<int> EliminarClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            using (TiendaMlgContext contexto = new TiendaMlgContext())
            {
                var cliente = contexto.Clientes.Find(_cliente.IdCliente);
                cliente.Activo = false;
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