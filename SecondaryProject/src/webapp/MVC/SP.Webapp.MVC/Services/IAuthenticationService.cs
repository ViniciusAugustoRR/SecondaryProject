using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Services
{
    public interface IAuthenticationService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);
    }

}
