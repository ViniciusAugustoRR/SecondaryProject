using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Catalog.API.Models;

namespace SP.Catalog.API.Data.Mappings
{
    public class ProdutoMappings : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.ToTable("Produtos");
        }
    }

}
