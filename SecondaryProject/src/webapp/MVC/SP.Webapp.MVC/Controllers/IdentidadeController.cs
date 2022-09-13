using Microsoft.AspNetCore.Mvc;
using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {
        [HttpGet]
        [Route("Registro")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("Registro")]
        public async Task<IActionResult> Registro(UsuarioRegistro UR)
        {
            var UL = new UsuarioLogin();
            return RedirectToAction("Login", UL);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UsuarioLogin UL)
        {

            return RedirectToAction("Index", "Home");
        }

    }
}
