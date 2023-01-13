using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace TiendaMLG.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            if(TempData["MENSAJE"] != null)
                ViewBag.mensaje = TempData["MENSAJE"].ToString();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(TiendaEntidades.Login login)
        {
            if (ModelState.IsValid)
            {
                var cliente = new TiendaEntidades.Cliente();

                if (login.UserName == "admin" || login.Password == "admin")
                {
                    cliente.Nombre = "Administrador";
                    cliente.Rol = "Administrador";
                    cliente.IdCliente = new Guid();
                }
                else
                {
                    cliente = await TiendaNegocio.Clientes.Login(login);

                    if(cliente != null)
                    {
                        cliente.Rol = "Cliente";
                    }
                    
                }

                if (cliente == null)
                {
                    TempData["MENSAJE"] = "No tienes credenciales correctas";

                    return RedirectToAction(nameof(Index));
                    //return View();
                }
                else
                {
                    
                    //DEBEMOS CREAR UNA IDENTIDAD (name y role)
                    //Y UN PRINCIPAL
                    //DICHA IDENTIDAD DEBEMOS COMBINARLA CON LA COOKIE DE 
                    //AUTENTIFICACION
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    //TODO USUARIO PUEDE CONTENER UNA SERIE DE CARACTERISTICAS
                    //LLAMADA CLAIMS.  DICHAS CARACTERISTICAS PODEMOS ALMACENARLAS
                    //DENTRO DE USER PARA UTILIZARLAS A LO LARGO DE LA APP
                    Claim claimUserName = new Claim(ClaimTypes.Name, cliente.Nombre);
                    Claim claimRole = new Claim(ClaimTypes.Role, cliente.Rol);
                    Claim claimIdUsuario = new Claim("IdCliente", cliente.IdCliente.ToString());

                    identity.AddClaim(claimUserName);
                    identity.AddClaim(claimRole);
                    identity.AddClaim(claimIdUsuario);

                    ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(45)
                    });

                    return RedirectToAction("Index", "Home");

                }
            }

            return View(login);

        }

        public IActionResult ErrorAcceso()
        {
            ViewData["MENSAJE"] = "Error de acceso";
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
