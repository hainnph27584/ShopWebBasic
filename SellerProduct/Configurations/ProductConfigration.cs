using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            // set khoa chinh
            builder.HasKey(p => p.Id);

            // set thuoc tinh
            builder.Property(p => p.Name).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Supplier).IsUnicode(true).IsFixedLength().HasMaxLength(100);// nvarchar(100)
        }
    }
}
