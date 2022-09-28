using SP.Core.Data;

namespace SP.Catalog.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> GetById(Guid id);

        void Add(Produto produto);

        void Update(Produto produto);

    }
}
