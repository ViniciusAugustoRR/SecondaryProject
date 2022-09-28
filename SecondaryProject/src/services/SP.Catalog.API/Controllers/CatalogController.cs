using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP.Catalog.API.Models;
using SP.WebApi.Core.Identidade;

namespace SP.Catalog.API.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/produtos")]
        public async Task<IEnumerable<Produto>> Index()
        {
            return await _produtoRepository.GetAll();
        }

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("catalog/produtos/{id}")]
        public async Task<Produto> ProdutoDetail(Guid Id)
        {
            return await _produtoRepository.GetById(Id);
        }




    }
}
