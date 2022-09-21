using SP.Webapp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using SP.Webapp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace SP.Webapp.MVC.Controllers
{
    public class IdentidadeController : MainController
    {

        private readonly IAuthenticationServiceCustom _authenticationService;

        public IdentidadeController(IAuthenticationServiceCustom authenticationService)
        {
            _authenticationService = authenticationService;
        }

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
            if (!ModelState.IsValid) return View(UR);

            var Response = await _authenticationService.Registro(UR);

            if (ResponseHasErrors(Response.ResponseResult)) return View(UR);
            
            await RealizarLogin(Response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UsuarioLogin UL, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(UL);

            var Response = await _authenticationService.Login(UL);

            if (ResponseHasErrors(Response.ResponseResult)) return View(UL);

            await RealizarLogin(Response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }


        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private async Task RealizarLogin(UsuarioResponseLogin responseLogin)
        {
            var token = GetFormatedToken(responseLogin.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", responseLogin.AccessToken));
            claims.AddRange(token.Claims);

            var ClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(ClaimsIdentity ),
                authProperties);
        }

        private static JwtSecurityToken GetFormatedToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        }

    }
}
