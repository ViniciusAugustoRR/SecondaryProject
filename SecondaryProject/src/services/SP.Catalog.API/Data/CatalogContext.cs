using Microsoft.EntityFrameworkCore;
using SP.Catalog.API.Models;
using SP.Core.Data;

namespace SP.Catalog.API.Data
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) 
            : base(options)
        {}

        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }


        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
