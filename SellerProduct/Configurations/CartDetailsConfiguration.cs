using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations
{
    public class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {

            builder.ToTable("CartDetails"); // Đặt tên bảng
            //builder.HasKey(p=>p.Id);// Set khóa chính
            builder.HasKey(p => new { p.Id }); // thiết lập khóa chính
            builder.HasOne(p => p.Cart).WithMany(p => p.CartDetails).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Product).WithMany(p => p.CartDetails).HasForeignKey(p => p.IdSp);
        }
    }
}
