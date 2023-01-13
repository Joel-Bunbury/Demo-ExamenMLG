using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMLG.Models.ViewModels;
using TiendaNegocio;

namespace TiendaMLG.Controllers
{
    public class TiendaController : Controller
    {
        [Authorize(Policy = "ADMINISTRADORES")]
        public async Task<IActionResult> Index()
        {
            var tiendas = TiendaNegocio.Tienda.ConsultarTiendas(null).ToList();
            return View( tiendas);
        }

        public async Task<IActionResult> Create(int IdTienda)
        {
            if(IdTienda != 0)
            {
                var tienda = await TiendaNegocio.Tienda.BuscarPorId(IdTienda);
                return View(tienda);
            }
            else
            {
                var tienda = new TiendaEntidades.Tienda();
                tienda.IdTienda = 0;
                return View(tienda);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TiendaEntidades.Tienda model)
        {
            if (ModelState.IsValid)
            {
                var result = 0;
                if (model.IdTienda == 0)
                {
                    result = await TiendaNegocio.Tienda.CrearTiendaAsync(model);
                }
                else
                {
                    result = await TiendaNegocio.Tienda.EditarTiendaAsync(model);
                }
                

                if(result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int IdTienda)
        {
            if (IdTienda != 0)
            {
                var tienda = await TiendaNegocio.Tienda.BuscarPorId(IdTienda);
                return View(tienda);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TiendaEntidades.Tienda model)
        {
            if (model.IdTienda != null)
            {
                var result = await TiendaNegocio.Tienda.EliminarTiendaAsync(model);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return View(model);
            }

            return View(model);
        }
    }
}
