using Microsoft.EntityFrameworkCore;
using SP.Catalog.API.Models;
using SP.Core.Data;

namespace SP.Catalog.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogContext _context;
        public IUnitOfWork unitOfWork => _context;

        public ProdutoRepository(CatalogContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
        public async Task<Produto> GetById(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }
        public void Add(Produto produto)
        {
            _context.Produtos.Add(produto);
        }
        public void Update(Produto produto)
        {
            _context.Produtos.Update(produto);
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}
