using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMLG.Models.ViewModels;
using TiendaNegocio;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TiendaMLG.Controllers
{
    public class ClienteController : Controller
    {
        [Authorize(Policy = "ADMINISTRADORES")]
        public async Task<IActionResult> Index()
        {
            var clientes = await TiendaNegocio.Clientes.ConsultarClientes(null);
            return View(clientes);
        }

        public async Task<IActionResult> Create(Guid IdCliente)
        {
            if(IdCliente != default(Guid))
            {
                var cliente = await TiendaNegocio.Clientes.BuscarPorId(IdCliente);
                cliente.Rol = "Cliente";
                return View(cliente);
            }
            else
            {
                var cliente = new TiendaEntidades.Cliente();
                cliente.IdCliente = default(Guid);
                cliente.Rol = "Cliente";
                return View(cliente);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TiendaEntidades.Cliente model)
        {
            if (ModelState.IsValid)
            {
                var result = 0;
                if (model.IdCliente == default(Guid))
                {
                    result = await TiendaNegocio.Clientes.CrearClienteAsync(model);
                }
                else
                {
                    result = await TiendaNegocio.Clientes.EditarClienteAsync(model);
                }
                

                if(result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid IdCliente)
        {
            if (IdCliente != default(Guid))
            {
                var tienda = await TiendaNegocio.Clientes.BuscarPorId(IdCliente);
                return View(tienda);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TiendaEntidades.Cliente model)
        {
            if (model.IdCliente != null)
            {
                var result = await TiendaNegocio.Clientes.EliminarClienteAsync(model);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Compras()
        {
            var articulos = await TiendaNegocio.Articulos.ConsultarArticulosCompra();
            return View(articulos);
        }

        public async Task<IActionResult> DescripcionProducto(Guid IdArticulo)
        {
            if (IdArticulo != default(Guid))
            {
                var articulo = await TiendaNegocio.Articulos.BuscarPorId(IdArticulo);
                articulo.Stock = 0;
                return View(articulo);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarCarrito(TiendaEntidades.Articulo model)
        {
            if (model.IdArticulo != null)
            {
                var ids = ((ClaimsIdentity)User.Identity).Claims
                          .Where(c => c.Type == "IdCliente")
                          .Select(c => c.Value);

                var item = new TiendaEntidades.CarritoDescripcion();
                item.IdArticulo = model.IdArticulo;
                item.IdCliente = new Guid(ids.FirstOrDefault());
                item.Cantidad = model.Stock;
                var result = await TiendaNegocio.Articulos.AgregarCarritoCompra(item);

                if (result > 0)
                    return RedirectToAction(nameof(Compras));
                else
                    return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> VerCarrito()
        {
            var Ids = ((ClaimsIdentity)User.Identity).Claims
                          .Where(c => c.Type == "IdCliente")
                          .Select(c => c.Value);

            var IdCliente = new Guid(Ids.FirstOrDefault());

            var articulos = await TiendaNegocio.Articulos.ConsultarArticulosCliente(IdCliente);
            return View(articulos);
        }

        public async Task<IActionResult> Pagar()
        {
            var Ids = ((ClaimsIdentity)User.Identity).Claims
                          .Where(c => c.Type == "IdCliente")
                          .Select(c => c.Value);

            var IdCliente = new Guid(Ids.FirstOrDefault());

            var pago = await TiendaNegocio.Articulos.PagarArticulosCliente(IdCliente);
            return View(pago);
        }
    }
}
