using TiendaEntidades;
using TiendaDatos;

namespace TiendaNegocio
{
    public static class Clientes
    {
        public static async Task<TiendaEntidades.Cliente> Login(TiendaEntidades.Login login)
        {
            return await TiendaDatos.Clientes.Login(login);
        }

        public static async Task<List<TiendaEntidades.Cliente>> ConsultarClientes(string nombre)
        {
            return await TiendaDatos.Clientes.ConsultarClientes(nombre);
        }

        public static async Task<TiendaEntidades.Cliente> BuscarPorId(Guid IdCliente) 
        {
            return await TiendaDatos.Clientes.BuscarPorId(IdCliente);
        }

        public static async Task<int> CrearClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            return await TiendaDatos.Clientes.CrearClienteAsync(_cliente);
        }

        public static async Task<int> EditarClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            return await TiendaDatos.Clientes.EditarClienteAsync(_cliente);
        }

        public static async Task<int> EliminarClienteAsync(TiendaEntidades.Cliente _cliente)
        {
            return await TiendaDatos.Clientes.EliminarClienteAsync(_cliente);
        }

    }
}