using Microsoft.Extensions.Options;
using SP.Webapp.MVC.Extensions;
using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Services
{
    public class AuthenticationService : Service, IAuthenticationServiceCustom
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _appSettings = settings.Value;

        }


        public async Task<UsuarioResponseLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            //var response = await _httpClient.PostAsync("https://localhost:44316/api/identidade/autenticar", loginContent);
            var response = await _httpClient.PostAsync($"{_appSettings.AuthenticationUrl}/api/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioResponseLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioResponseLogin>(response);
        }

        public async Task<UsuarioResponseLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            //var response = await _httpClient.PostAsync("https://localhost:44316/api/identidade/registrar", registroContent);
            var response = await _httpClient.PostAsync($"{_appSettings.AuthenticationUrl}/api/identidade/registrar", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioResponseLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioResponseLogin>(response);
        }



    }

}

