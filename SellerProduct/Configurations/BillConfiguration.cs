using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellerProduct.Models;
namespace SellerProduct.Configurations

{
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill"); // Đặt tên bảng
            //builder.HasKey(p=>p.Id);// Set khóa chính
            builder.HasKey(p => new { p.Id }); // thiết lập khóa chính
            // Thiết lập cho các thuộc tính
            builder.Property(p => p.Status).HasColumnType("int").IsRequired();// int not null;
            builder.HasOne(x=>x.User).WithMany(x => x.Bill).HasForeignKey(x=>x.UserId);
            
        }
    }
}
