using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Services
{
    public interface IAuthenticationServiceCustom
    {
        Task<UsuarioResponseLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioResponseLogin> Registro(UsuarioRegistro usuarioRegistro);
    }

}
