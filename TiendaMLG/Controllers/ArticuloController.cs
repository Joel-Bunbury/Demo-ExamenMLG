using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaMLG.Models.ViewModels;
using TiendaNegocio;

namespace TiendaMLG.Controllers
{
    public class ArticuloController : Controller
    {
        public async Task<IActionResult> Index(int IdTienda)
        {
            var articulos = await TiendaNegocio.Articulos.ConsultarArticulos(IdTienda);
            ViewBag.IdTienda = IdTienda;
            return View(articulos);
        }

        public async Task<IActionResult> Create(Guid IdArticulo, int IdTienda)
        {
            if(IdArticulo != default(Guid) && IdTienda != 0)
            {
                var articulo = await TiendaNegocio.Articulos.BuscarPorId(IdArticulo, IdTienda);
                articulo.Imagen = "Imagen";
                return View(articulo);
            }
            else
            {
                var articulo = new TiendaEntidades.Articulo();
                articulo.IdArticulo = default(Guid);
                articulo.IdTienda = IdTienda;
                articulo.Imagen = "Imagen";
                return View(articulo);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TiendaEntidades.Articulo model)
        {
            if (ModelState.IsValid)
            {
                var result = 0;
                if (model.IdArticulo == default(Guid))
                {
                    result = await TiendaNegocio.Articulos.CrearArticuloAsync(model);
                }
                else
                {
                    result = await TiendaNegocio.Articulos.EditarArticuloAsync(model);
                }
                

                if(result > 0)
                    return RedirectToAction(nameof(Index), new { IdTienda = model.IdTienda });
                else
                    return View(model);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid IdArticulo,int IdTienda)
        {
            if (IdArticulo != default(Guid))
            {
                var tienda = await TiendaNegocio.Articulos.BuscarPorId(IdArticulo, IdTienda);
                return View(tienda);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TiendaEntidades.Articulo model)
        {
            if (model.IdArticulo != null)
            {
                var result = await TiendaNegocio.Articulos.EliminarArticuloAsync(model);

                if (result > 0)
                    return RedirectToAction(nameof(Index), new { IdTienda = model.IdTienda });
                else
                    return View(model);
            }

            return View(model);
        }
    }
}
