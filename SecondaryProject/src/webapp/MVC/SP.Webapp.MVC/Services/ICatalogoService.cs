using Microsoft.Extensions.Options;
using SP.Webapp.MVC.Extensions;
using SP.Webapp.MVC.Models;

namespace SP.Webapp.MVC.Services
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAll();
        Task<ProdutoViewModel> GetById(Guid id);
    }

    public class CatalogoService : Service, ICatalogoService
    {
        private readonly HttpClient _httpClient;


        public CatalogoService(HttpClient httpClient, 
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<ProdutoViewModel>>(response);
        }

        public async Task<ProdutoViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalogo/produtos/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<ProdutoViewModel>(response);
        }

    }

}
